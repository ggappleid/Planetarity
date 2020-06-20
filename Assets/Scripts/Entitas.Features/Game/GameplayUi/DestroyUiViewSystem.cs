using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

namespace Entitas.Features.Game.GameplayUi
{
    public class DestroyUiViewSystem : ReactiveSystem<GameEntity>, ITearDownSystem
    {
        private readonly GameContext _game;
        
        public DestroyUiViewSystem(GameContext game) : base(game)
        {
            _game = game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Planet.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasUiView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (!entity.hasUiView)
                {
                    continue;
                }
                
                var uiView = entity.uiView;
                var go = uiView.Value;

                // We've run out of health before destroy
                var health = entity.hasHealth ? entity.health.Value : 0f;
                
                go.Unlink();
                Object.Destroy(go);
            }
        }

        public void TearDown()
        {
            var entities = _game.GetGroup(GameMatcher.UiView);

            foreach (var entity in entities)
            {
                if (entity.uiView.Value == null)
                {
                    continue;
                }
                entity.uiView.Value.Unlink();
            }
        }
    }
}
