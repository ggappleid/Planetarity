using System.Collections.Generic;

namespace Entitas.Features.Game.Gameplay
{
    public class DestroyObjectOnZeroHealthSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;
            
        public DestroyObjectOnZeroHealthSystem(GameContext game) : base(game)
        {
            _game = game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Health);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasHealth;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.health.Value > 0f)
                {
                    continue;
                }
                
                // Destroy rocket
                entity.isRocket = false;
                
                // Destroy planet
                if (entity.hasPlanet)
                {
                    // Kill Player
                    var playerE = _game.GetPlayerEntityByPlanet(entity);
                    playerE.ReplacePlayer(playerE.player.Planet, playerE.player.Type, playerE.player.BotStrength, false);
                    // Remove planet
                    entity.RemovePlanet();
                }
            }
        }
    }
}
