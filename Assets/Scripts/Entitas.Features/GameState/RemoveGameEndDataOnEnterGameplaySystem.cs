using System.Collections.Generic;
using UnityEngine;

namespace Entitas.Features.GameState
{
    public class RemoveGameEndDataOnEnterGameplaySystem : ReactiveSystem<GameInfoEntity>
    {
        private readonly GameInfoContext _gameInfo;
    
        public RemoveGameEndDataOnEnterGameplaySystem(GameInfoContext gameInfo) : base(gameInfo)
        {
            _gameInfo = gameInfo;
        }

        protected override ICollector<GameInfoEntity> GetTrigger(IContext<GameInfoEntity> context)
        {
            return context.CreateCollector(GameInfoMatcher.CurrentState);
        }

        protected override bool Filter(GameInfoEntity entity)
        {
            return _gameInfo.hasCurrentState && 
                   _gameInfo.currentState.Value == global::GameState.Gameplay;
        }

        protected override void Execute(List<GameInfoEntity> entities)
        {
            if (_gameInfo.hasGameEnded)
            {
                _gameInfo.RemoveGameEnded();
            }
        }
    }
}
