using System;
using System.Collections.Generic;
using System.Linq;

namespace Entitas.Features.Game.Gameplay
{
    public class InitWorldWithPlanetsOnGameStartSystem : ReactiveSystem<GameInfoEntity>
    {
        private readonly GameContext _game;
        private readonly GameInfoContext _gameInfo;
        private readonly Random _rand;
        
        private readonly CannonComponent[] _cannons =
        {
            new CannonComponent { ThrowPower = 7.5f, CooldownTime = 1f, FirePower = (int)Consts.RocketFirePower.JustScratch },
            new CannonComponent { ThrowPower = 6.5f, CooldownTime = 2f, FirePower = (int)Consts.RocketFirePower.ReallyHurts },
            new CannonComponent { ThrowPower = 5.5f, CooldownTime = 3f, FirePower = (int)Consts.RocketFirePower.SuperDeadly }
        };
        
        public InitWorldWithPlanetsOnGameStartSystem(GameInfoContext gameInfo, GameContext game) : base(gameInfo)
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
            var sunE = _game.CreateEntity();
            sunE.ReplacePlanet("Sun", 0f, 0f);
            sunE.ReplaceHealth(Consts.SunHealth);
            sunE.ReplaceMass(750f);
            
            var cannons = _cannons.ToList();
            cannons.Shuffle();
            
            for (var i = 0; i < Consts.PlanetNames.Length; i++)
            {
                var rotationRadius = (i + 1) * 1.33f; // Do not exceed 4 (for 3 planets)
                var randomValue = (float)_rand.NextDouble();
                var sign = Math.Sign(randomValue - 0.5f);
                var rotationSpeed = sign * (20f + randomValue * 20f);
                
                var planetE = _game.CreateEntity();
                planetE.ReplacePlanet(Consts.PlanetNames[i], rotationRadius, rotationSpeed);
                planetE.ReplaceHealth(1000f);
                planetE.ReplaceMass(250f);
                planetE.ReplaceComponent(GameComponentsLookup.Cannon, cannons[i]);
            }
        }
    }
}
