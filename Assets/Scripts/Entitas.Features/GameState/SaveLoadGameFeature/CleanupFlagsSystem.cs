namespace Entitas.Features.GameState.SaveLoadGameFeature
{
    public class CleanupFlagsSystem : ICleanupSystem
    {
        private readonly IGroup<GameInfoEntity> _saveFlagsGroup;
        private readonly IGroup<GameInfoEntity> _loadFlagsGroup;

        public CleanupFlagsSystem(GameInfoContext gameInfo)
        {
            _saveFlagsGroup = gameInfo.GetGroup(GameInfoMatcher.SaveGame);
            _loadFlagsGroup = gameInfo.GetGroup(GameInfoMatcher.LoadGame);
        }

        public void Cleanup()
        {
            foreach (var entity in _saveFlagsGroup.GetEntities())
            {
                entity.isSaveGame = false;
                entity.Destroy();
            }
            foreach (var entity in _loadFlagsGroup.GetEntities())
            {
                entity.isLoadGame = false;
                entity.Destroy();
            }
        }
    }
}