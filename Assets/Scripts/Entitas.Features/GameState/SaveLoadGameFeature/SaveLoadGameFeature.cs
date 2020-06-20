namespace Entitas.Features.GameState.SaveLoadGameFeature
{
    public class SaveLoadGameFeature : Feature
    {
        public SaveLoadGameFeature(Contexts contexts, 
            IDataSaver dataSaver, IDataLoader<GameBoardState> dataLoader) : base("Save Load Game Feature")
        {
            Add(new SaveGameSystem(contexts.gameInfo, contexts.game, dataSaver));
            Add(new LoadGameSystem(contexts.gameInfo, contexts.game, dataLoader));

            Add(new CleanupFlagsSystem(contexts.gameInfo));
        }
    }
}
