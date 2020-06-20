namespace Entitas.Features.Game.Gameplay
{
    public class GameplayFeature : Feature
    {
        public GameplayFeature(Contexts contexts)
        {
            Add(new InitWorldWithPlanetsOnGameStartSystem(contexts.gameInfo, contexts.game));
            Add(new InitWorldWithPlayersOnGameStartSystem(contexts.gameInfo, contexts.game));
            Add(new CalculatePlanetPositionExecuteSystem(contexts.game));
            
            Add(new AiShootSystem(contexts.game));
            Add(new ConvertInputToShootSystem(contexts.input, contexts.game));
            Add(new ShootRocketsSystem(contexts.game));

            Add(new UpdateRocketPropertiesSystem(contexts.game));
            Add(new ApplyGravityToRocketsSystem(contexts.game));
            Add(new DestroyRocketOnLeaveAreaSystem(contexts.game));
            Add(new DecreaseHealthOnCollisionSystem(contexts.game));
            Add(new DestroyObjectOnZeroHealthSystem(contexts.game));
            Add(new ProcessCooldownTimerSystem(contexts.game));
        }
    }
}
