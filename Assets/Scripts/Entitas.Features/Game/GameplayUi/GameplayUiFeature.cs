namespace Entitas.Features.Game.GameplayUi
{
    public class GameplayUiFeature : Feature
    {
        public GameplayUiFeature(Contexts contexts)
        {
            Add(new CreateViewSystem(contexts.game));
            Add(new DestroyViewSystem(contexts.game));
            
            Add(new CreateUiViewSystem(contexts.game));
            Add(new DestroyUiViewSystem(contexts.game));
        }
    }
}
