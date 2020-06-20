using UnityEngine;

namespace Entitas.Features.Input
{
    public class HandleEscButtonSystem : IExecuteSystem
    {
        private readonly GameInfoContext _gameInfo;
        
        public HandleEscButtonSystem(GameInfoContext gameInfo)
        {
            _gameInfo = gameInfo;
        }
        
        public void Execute()
        {
            if (!UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                return;
            }
            
            if (!_gameInfo.hasCurrentState)
            {
                _gameInfo.ReplaceCurrentState(global::GameState.Menu);
                return;
            }
                
            _gameInfo.ReplaceCurrentState(_gameInfo.currentState.Value == global::GameState.Gameplay
                ? global::GameState.Menu
                : global::GameState.Gameplay);
        }
    }
}
