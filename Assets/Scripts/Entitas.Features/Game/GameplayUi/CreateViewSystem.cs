using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

namespace Entitas.Features.Game.GameplayUi
{
    public class CreateViewSystem : ReactiveSystem<GameEntity>
    {
        public CreateViewSystem(GameContext game) : base(game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.AnyOf(
                    GameMatcher.Planet, 
                    GameMatcher.Rocket));
        }

        protected override bool Filter(GameEntity entity)
        {
            return !entity.hasView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var parentView = SolarSystemView.Instance;

                var prefab = entity.hasPlanet 
                    ? parentView.planetPrefab 
                    : parentView.rocketPrefab;
                
                var newPrefabGo = parentView.InstantiatePrefab(prefab);
                var view = newPrefabGo.GetComponent<IView>();
        
                if (view == null)
                {
                    Object.Destroy(newPrefabGo);
                    return;
                }

                entity.ReplaceView(newPrefabGo);
                var entityLink = newPrefabGo.Link(entity);
                view.Init(entityLink);
            }
        }
    }
}
