
using UnityEngine;

namespace Entitas.Features.Game.Gameplay
{
    public class ProcessCooldownTimerSystem : IExecuteSystem, ICleanupSystem
    {
        private readonly IGroup<GameEntity> _cooldownTimers;

        public ProcessCooldownTimerSystem(GameContext game)
        {
            _cooldownTimers = game.GetGroup(GameMatcher.CooldownTimer);
        }
        
        public void Execute()
        {
            foreach (var timerE in _cooldownTimers.GetEntities())
            {
                var currTimer = timerE.cooldownTimer.Value - Time.deltaTime;
                timerE.ReplaceCooldownTimer(currTimer);
            }
        }

        public void Cleanup()
        {
            foreach (var timerE in _cooldownTimers.GetEntities())
            {
                if (timerE.cooldownTimer.Value <= 0f)
                {
                    timerE.RemoveCooldownTimer();
                }
            }
        }
    }
}
