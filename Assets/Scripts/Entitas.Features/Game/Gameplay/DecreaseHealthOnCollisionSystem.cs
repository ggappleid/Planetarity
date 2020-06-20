using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entitas.Features.Game.Gameplay
{
    public class DecreaseHealthOnCollisionSystem : ReactiveSystem<GameEntity>, ICleanupSystem
    {
        private readonly List<GameEntity> _entitiesToCleanup;
        private readonly IGroup<GameEntity> _views;
        
        public DecreaseHealthOnCollisionSystem(GameContext game) : base(game)
        {
            _entitiesToCleanup= new List<GameEntity>();
            _views = game.GetGroup(GameMatcher.View);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Collision);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasCollision;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var originalE = _views
                    .GetEntities()
                    .FirstOrDefault(i => i.view.Value == entity.collision.originalObj);
                
                var hitE = _views
                    .GetEntities()
                    .FirstOrDefault(i => i.view.Value == entity.collision.hitObj);

                Physics.IgnoreCollision(
                    originalE.view.Value.GetComponentInChildren<Collider>(),
                    hitE.view.Value.GetComponentInChildren<Collider>());
                
                var originalHealth = originalE.hasHealth ? originalE.health.Value : 0f;
                var originFirePower = originalE.hasFirePower && originalHealth > 0f ? originalE.firePower.Value : 0f;
                
                var hitHealth = hitE.hasHealth ? hitE.health.Value : 0f;
                var hitFirePower = hitE.hasFirePower && hitHealth > 0f ? hitE.firePower.Value : 0f;
                
                var firePower = Mathf.Max(originFirePower, hitFirePower);

                if (firePower <= 0f)
                {
                    _entitiesToCleanup.Add(entity);
                    continue;
                }
                
                if (originalE.hasHealth)
                {
                    var currValue = originalE.health.Value;
                    currValue = Mathf.Clamp(currValue - firePower, 0f, Consts.SunHealth);
                    originalE.ReplaceHealth(currValue);
                }

                if (hitE.hasHealth)
                {
                    var currValue = hitE.health.Value;
                    currValue = Mathf.Clamp(currValue - firePower, 0f, Consts.SunHealth);
                    hitE.ReplaceHealth(currValue);
                }
                
                _entitiesToCleanup.Add(entity);
            }
        }

        public void Cleanup()
        {
            foreach (var entity in _entitiesToCleanup)
            {
                entity.Destroy();
            }
            _entitiesToCleanup.Clear();
        }
    }
}
