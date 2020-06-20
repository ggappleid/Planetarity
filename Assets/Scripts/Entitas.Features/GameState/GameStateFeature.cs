namespace Entitas.Features.GameState
{
    public class GameStateFeature : Feature
    {
        public GameStateFeature(Contexts contexts, Feature gameplayFeature)
        {
            Add(new SetInitialGameStateSystem(contexts.gameInfo));
            Add(new RemoveGameEndDataOnEnterGameplaySystem(contexts.gameInfo));
            Add(new PlayPauseGameplaySystem(contexts.gameInfo, gameplayFeature));
            Add(new StartGameSystem(contexts.gameInfo));
            Add(new EndGameSystem(contexts.game, contexts.gameInfo));
            Add(new GoToMenuOnGameEndSystem(contexts.gameInfo));
            Add(new CleanupGameDataOnGameEndSystem(contexts.gameInfo, contexts.game));
            Add(new RemoveGameStartOnGameEnd(contexts.gameInfo));
        }
    }
}
