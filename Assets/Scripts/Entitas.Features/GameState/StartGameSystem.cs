using System.Collections.Generic;

namespace Entitas.Features.GameState
{
    public class StartGameSystem : ReactiveSystem<GameInfoEntity>
    {
        private readonly GameInfoContext _gameInfo;
    
        public StartGameSystem(GameInfoContext gameInfo) : base(gameInfo)
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
            if (!_gameInfo.hasGameStart)
            {
                var complexity = _gameInfo.hasPreferredComplexity
                    ? _gameInfo.preferredComplexity.GameComplexity
                    : GameComplexityType.Random;
                
                _gameInfo.ReplaceGameStart(true, complexity);
            }
        }
    }
}
