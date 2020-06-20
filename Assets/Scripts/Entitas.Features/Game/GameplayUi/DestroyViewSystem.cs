using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

namespace Entitas.Features.Game.GameplayUi
{
    public class DestroyViewSystem : ReactiveSystem<GameEntity>, ICleanupSystem, ITearDownSystem
    {
        private readonly GameContext _game;
        private readonly List<GameEntity> _entitiesToDestroy;
        
        public DestroyViewSystem(GameContext game) : base(game)
        {
            _game = game;
            _entitiesToDestroy = new List<GameEntity>();
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.Planet.Removed(), 
                    GameMatcher.Rocket.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (!entity.hasView)
                {
                    continue;
                }
                
                var view = entity.view;
                var go = view.Value;
                var mono = go.GetComponent<IView>();

                // We've run out of health before destroy
                var health = entity.hasHealth ? entity.health.Value : 0f;
                var isLoudDestroy = health <= 0f;
                
                mono.BeforeDestroy(isLoudDestroy);
                go.Unlink();
                Object.Destroy(go);
                
                _entitiesToDestroy.Add(entity);
            }
        }

        public void Cleanup()
        {
            foreach (var entity in _entitiesToDestroy)
            {
                entity.Destroy();
            }
            _entitiesToDestroy.Clear();
        }

        public void TearDown()
        {
            var entities = _game.GetGroup(GameMatcher.View);

            foreach (var entity in entities)
            {
                if (entity.view.Value == null)
                {
                    continue;
                }
                entity.view.Value.Unlink();
            }
        }
    }
}
