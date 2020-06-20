using System.Collections.Generic;

namespace Entitas.Features.GameState
{
    public class RemoveGameStartOnGameEnd : ReactiveSystem<GameInfoEntity>
    {
        private readonly GameInfoContext _gameInfo;

        public RemoveGameStartOnGameEnd(GameInfoContext gameInfo) : base(gameInfo)
        {
            _gameInfo = gameInfo;
        }

        protected override ICollector<GameInfoEntity> GetTrigger(IContext<GameInfoEntity> context)
        {
            return context.CreateCollector(GameInfoMatcher.GameEnded);
        }

        protected override bool Filter(GameInfoEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameInfoEntity> entities)
        {
            if (_gameInfo.hasGameStart)
            {
                _gameInfo.RemoveGameStart();
            }
        }
    }
}