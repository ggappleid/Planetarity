namespace Entitas.Features.GameState
{
    public class SetInitialGameStateSystem : IInitializeSystem
    {
        private readonly GameInfoContext _gameInfo;
        
        public SetInitialGameStateSystem(GameInfoContext gameInfo)
        {
            _gameInfo = gameInfo;
        }
        
        public void Initialize()
        {
            _gameInfo.ReplaceCurrentState(global::GameState.Menu);
        }
    }
}
