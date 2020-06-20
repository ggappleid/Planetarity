using System.Collections.Generic;
using UnityEngine;

namespace Entitas.Features.GameState
{
    public class GoToMenuOnGameEndSystem : ReactiveSystem<GameInfoEntity>
    {
        private readonly GameInfoContext _gameInfo;
    
        public GoToMenuOnGameEndSystem(GameInfoContext gameInfo) : base(gameInfo)
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
            _gameInfo.ReplaceCurrentState(global::GameState.Menu);
        }
    }
}
