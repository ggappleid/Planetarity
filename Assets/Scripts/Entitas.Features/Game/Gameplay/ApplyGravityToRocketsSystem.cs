using UnityEngine;

namespace Entitas.Features.Game.Gameplay
{
    public class ApplyGravityToRocketsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _rockets;
        private readonly IGroup<GameEntity> _planets;
        
        public ApplyGravityToRocketsSystem(GameContext game)
        {
            _planets = game.GetGroup(
                GameMatcher.AllOf(
                    GameMatcher.Mass, 
                    GameMatcher.Position));
            
            _rockets = game.GetGroup(
                GameMatcher.AllOf(
                    GameMatcher.Rocket, 
                    GameMatcher.Velocity, 
                    GameMatcher.Position,
                    GameMatcher.View));
        }
        
        public void Execute()
        {
            foreach (var rocketE in _rockets)
            {
                var force = Vector3.zero;

                foreach (var planetE in _planets)
                {
                    if (planetE == rocketE)
                    {
                        continue;
                    }

                    var massDirectionVector = planetE.position.Value - rocketE.position.Value;
                    // Debug.Log(massDirectionVector);
                    // Debug.DrawRay(rocketE.position.Value, massDirectionVector, Color.green);
                    var sqrDistance = massDirectionVector.sqrMagnitude;
                    var massDirectionApplyForceVector = massDirectionVector.normalized * (1f / sqrDistance);
                    // Debug.Log(massDirectionApplyForceVector);
                    force += massDirectionApplyForceVector * planetE.mass.Value * Time.deltaTime;
                }

                var view = rocketE.view.Value.GetComponent<IView>();
                view.Rigidbody.AddForce(force);
            }
        }
    }
}
