using System.Collections.Generic;
using UnityEngine;

namespace Entitas.Features.GameState.SaveLoadGameFeature
{
    public class LoadGameSystem : ReactiveSystem<GameInfoEntity>
    {
        private readonly GameInfoContext _gameInfo;
        private readonly GameContext _game;
        private readonly IDataLoader<GameBoardState> _dataLoader;

        private readonly IGroup<GameEntity> _planets;
        private readonly IGroup<GameEntity> _rockets;
        private readonly IGroup<GameEntity> _players;

        public LoadGameSystem(GameInfoContext gameInfo, GameContext game, IDataLoader<GameBoardState> dataLoader) : base(gameInfo)
        {
            _gameInfo = gameInfo;
            _game = game;
            _dataLoader = dataLoader;
            
            _planets = game.GetGroup(GameMatcher.Planet);
            _rockets = game.GetGroup(GameMatcher.Rocket);
            _players = game.GetGroup(GameMatcher.Player);
        }

        protected override ICollector<GameInfoEntity> GetTrigger(IContext<GameInfoEntity> context)
        {
            return context.CreateCollector(GameInfoMatcher.LoadGame);
        }

        protected override bool Filter(GameInfoEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameInfoEntity> entities)
        {
            var cardGameBoardData = _dataLoader.Load();

            if (cardGameBoardData == null)
            {
                Debug.LogWarning("Nothing to load!");
                return;
            }

            CleanOldData();
            LoadNewData(cardGameBoardData);
            
            _gameInfo.ReplaceGameStart(false, GameComplexityType.Random);
        }

        private void CleanOldData()
        {
            foreach (var planetE in _planets.GetEntities())
            {
                if (planetE.hasView)
                {
                    Object.Destroy(planetE.view.Value);
                }
                if (planetE.hasUiView)
                {
                    Object.Destroy(planetE.uiView.Value);
                }
                planetE.Destroy();
            }
            
            foreach (var rocketE in _rockets.GetEntities())
            {
                if (rocketE.hasView)
                {
                    Object.Destroy(rocketE.view.Value);
                }
                rocketE.Destroy();
            }
            
            foreach (var playerE in _players.GetEntities())
            {
                playerE.Destroy();
            }
        }

        private void LoadNewData(GameBoardState gameBoardState)
        {
            foreach (var planet in gameBoardState.Planets)
            {
                var planetE = _game.CreateEntity();
                planetE.ReplaceComponent(GameComponentsLookup.Planet, planet.Planet);
                planetE.ReplaceComponent(GameComponentsLookup.Position, planet.Position);
                planetE.ReplaceComponent(GameComponentsLookup.Mass, planet.Mass);
                planetE.ReplaceComponent(GameComponentsLookup.Health, planet.Health);
                planetE.ReplaceComponent(GameComponentsLookup.Cannon, planet.Cannon); 
                planetE.ReplaceComponent(GameComponentsLookup.CooldownTimer, planet.CooldownTimer); 
            }
            
            foreach (var rocket in gameBoardState.Rockets)
            {
                var rocketE = _game.CreateEntity();
                rocketE.ReplaceComponent(GameComponentsLookup.FirePower, rocket.FirePower);
                rocketE.ReplaceComponent(GameComponentsLookup.Health, rocket.Health);
                rocketE.ReplaceComponent(GameComponentsLookup.Position, rocket.Position);
                rocketE.ReplaceComponent(GameComponentsLookup.Velocity, rocket.Velocity);
                rocketE.isRocket = true;
            }
            
            foreach (var player in gameBoardState.Players)
            {
                var playerE = _game.CreateEntity();
                playerE.ReplaceComponent(GameComponentsLookup.Player, player.Player);
            }
        }
    }
}