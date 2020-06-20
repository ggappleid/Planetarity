using System.Collections.Generic;
using System.Linq;

namespace Entitas.Features.Game.Gameplay
{
    public class ShootRocketsSystem : ReactiveSystem<GameEntity>, ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _shootEs;
        
        public ShootRocketsSystem(GameContext game) : base(game)
        {
            _game = game;
            _shootEs = _game.GetGroup(GameMatcher.Shoot);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Shoot);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var shootE in entities)
            {
                // Search planet that is shooting
                var planetE = _game
                    .GetEntities(GameMatcher.Planet)
                    .First(i => string.Equals(i.planet.Name, shootE.shoot.PlanetName));

                // Do not allow to shot until cooldown timer will not disappear
                if (planetE.hasCooldownTimer)
                {
                    continue;
                }
                
                // Add cooldown timer
                planetE.ReplaceCooldownTimer(planetE.cannon.CooldownTime);
                
                // Calculate correct rocket position
                var target = shootE.shoot.Target;
                var aimDirection = (target - planetE.position.Value).normalized;
                var throwBias = aimDirection * 0.6f;
                var rocketPosition = planetE.position.Value + throwBias;
            
                // Create cocket
                var rocketE = _game.CreateEntity();
                rocketE.ReplaceFirePower(shootE.shoot.FirePower);
                rocketE.ReplacePosition(rocketPosition);
                rocketE.ReplaceHealth(1f);
                rocketE.ReplaceVelocity(aimDirection * shootE.shoot.ThrowPower);
                rocketE.isRocket = true;
            }
        }

        public void Cleanup()
        {
            foreach (var entity in _shootEs.GetEntities())
            {
                entity.Destroy();
            }
        }
    }
}
