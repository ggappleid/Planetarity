using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

namespace Entitas.Features.Game.GameplayUi
{
    public class CreateUiViewSystem : ReactiveSystem<GameEntity>
    {
        public CreateUiViewSystem(GameContext game) : base(game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Planet);
        }

        protected override bool Filter(GameEntity entity)
        {
            return !entity.hasUiView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (string.Equals(entity.planet.Name,"Sun"))
                {
                    continue;
                }
                
                var mainView = SolarSystemView.Instance;
                var newPrefabGo = mainView.InstantiateUiPrefab(mainView.planetHudPrefab);
                
                var view = newPrefabGo.GetComponent<PlanetHudView>();
        
                if (view == null)
                {
                    Object.Destroy(newPrefabGo);
                    return;
                }

                entity.ReplaceUiView(newPrefabGo);
                var entityLink = newPrefabGo.Link(entity);
                view.Init(entityLink);
            }
        }
    }
}
