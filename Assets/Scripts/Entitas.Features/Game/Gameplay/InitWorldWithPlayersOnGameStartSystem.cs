using System;
using System.Collections.Generic;
using System.Linq;

namespace Entitas.Features.Game.Gameplay
{
    public class InitWorldWithPlayersOnGameStartSystem : ReactiveSystem<GameInfoEntity>
    {
        private readonly GameContext _game;
        private readonly GameInfoContext _gameInfo;
        private readonly Random _rand;

        public InitWorldWithPlayersOnGameStartSystem(GameInfoContext gameInfo, GameContext game) : base(gameInfo)
        {
            _game = game;
            _gameInfo = gameInfo;
            _rand = new Random();
        }
        
        protected override ICollector<GameInfoEntity> GetTrigger(IContext<GameInfoEntity> context)
        {
            return context.CreateCollector(GameInfoMatcher.GameStart);
        }

        protected override bool Filter(GameInfoEntity entity)
        {
            return _gameInfo.hasGameStart && 
                   _gameInfo.gameStart.IsNew;
        }

        protected override void Execute(List<GameInfoEntity> entities)
        {
            Initialize();
        }
    
        private void Initialize()
        {
            var planets = Consts.PlanetNames.ToList();
            planets.Shuffle();

            var playerE = _game.CreateEntity();
            playerE.ReplacePlayer(planets[0], PlayerType.Player, 1f, true);

            for (var i = 1; i < Consts.PlanetNames.Length; i++)
            {
                var botComplexity = ConvertGameComplexityToBotStrength(_gameInfo.gameStart.GameComplexity);
                var botE = _game.CreateEntity();
                botE.ReplacePlayer(planets[i], PlayerType.Bot, botComplexity, true);
            }
        }

        private float ConvertGameComplexityToBotStrength(GameComplexityType gameComplexity)
        {
            if (gameComplexity == GameComplexityType.Random)
            {
                return (float) _rand.NextDouble();
            }
            
            var complexityTypeCount = Enum.GetNames(typeof(GameComplexityType)).Length - 1;
            var complexityScale = 1f / complexityTypeCount;
            var complexityIdx = (int)gameComplexity - 1;
            
            return (float) _rand.NextDouble() * complexityScale + complexityIdx * complexityScale;
        }
    }
}
