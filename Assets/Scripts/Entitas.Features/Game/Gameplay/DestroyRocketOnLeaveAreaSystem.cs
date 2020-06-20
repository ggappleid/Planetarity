using System.Collections.Generic;
using UnityEngine;

namespace Entitas.Features.Game.Gameplay
{
    public class DestroyRocketOnLeaveAreaSystem : ReactiveSystem<GameEntity>, ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly List<GameEntity> _rocketsToDestroy;
        
        public DestroyRocketOnLeaveAreaSystem(GameContext game) : base(game)
        {
            _game = game;
            _rocketsToDestroy = new List<GameEntity>();
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.AllOf(
                    GameMatcher.Rocket, 
                    GameMatcher.Position));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (Mathf.Abs(entity.position.Value.x) > 10f ||
                    Mathf.Abs(entity.position.Value.z) > 10f)
                {
                    _rocketsToDestroy.Add(entity);
                }
            }
        }

        public void Cleanup()
        {
            foreach (var entity in _rocketsToDestroy)
            {
                entity.isRocket = false;
            }
            _rocketsToDestroy.Clear();
        }
    }
}
