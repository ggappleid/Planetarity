using System.Collections.Generic;
using UnityEngine;

namespace Entitas.Features.GameState
{
    public class PlayPauseGameplaySystem : ReactiveSystem<GameInfoEntity>
    {
        private readonly Feature _gameplayFeature;
        private readonly GameInfoContext _gameInfo;
    
        public PlayPauseGameplaySystem(GameInfoContext gameInfo, Feature gameplayFeature) : base(gameInfo)
        {
            _gameInfo = gameInfo;
            _gameplayFeature = gameplayFeature;
        }

        protected override ICollector<GameInfoEntity> GetTrigger(IContext<GameInfoEntity> context)
        {
            return context.CreateCollector(GameInfoMatcher.CurrentState);
        }

        protected override bool Filter(GameInfoEntity entity)
        {
            return entity.hasCurrentState;
        }

        protected override void Execute(List<GameInfoEntity> entities)
        {
            var isPlay = _gameInfo.currentState.Value == global::GameState.Gameplay;

            if (isPlay)
            {
                Time.timeScale = 1f;
                _gameplayFeature.ActivateReactiveSystems();
            }
            else
            {
                Time.timeScale = 0f;
                _gameplayFeature.DeactivateReactiveSystems();
            }
        }
    }
}
