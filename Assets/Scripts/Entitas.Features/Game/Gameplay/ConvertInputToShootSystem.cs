using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entitas.Features.Game.Gameplay
{
    public class ConvertInputToShootSystem : ReactiveSystem<InputEntity>
    {
        private readonly GameContext _game;
        
        public ConvertInputToShootSystem(InputContext input, GameContext game) : base(input)
        {
            _game = game;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.ScreenClick);
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasScreenClick;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var entity = entities.First();
            var mousePos = entity.screenClick.Value;
            var desiredTarget = SolarSystemView.Instance.mainCamera
                .ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

            var playerE = _game.GetPlayerEntity();

            if (playerE == null ||
                !playerE.player.IsAlive)
            {
                return;
            }
            
            var planetE = _game.GetPlanetEntity(playerE);
            
            _game
                .CreateEntity()
                .ReplaceShoot(planetE.planet.Name, desiredTarget, planetE.cannon.FirePower, planetE.cannon.ThrowPower);
        }
    }
}
