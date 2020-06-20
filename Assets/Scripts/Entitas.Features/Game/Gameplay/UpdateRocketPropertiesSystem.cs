namespace Entitas.Features.Game.Gameplay
{
    public class UpdateRocketPropertiesSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _rockets;
        
        public UpdateRocketPropertiesSystem(GameContext game)
        {
            _rockets = game.GetGroup(GameMatcher.Rocket);
        }
        
        public void Execute()
        {
            foreach (var rocketE in _rockets)
            {
                if (!rocketE.hasView)
                {
                    continue;
                }

                var viewGo = rocketE.view.Value;
                var view = viewGo.GetComponent<IView>();
                
                rocketE.ReplacePosition(view.Transform.position);
                rocketE.ReplaceVelocity(view.Rigidbody.velocity);
            }
        }
    }
}
