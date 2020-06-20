
namespace Entitas.Features.Input
{
    public class InputFeature : Feature
    {
        public InputFeature(Contexts contexts) : base("Input Feature")
        {
            Add(new HandleMouseClickExecuteSystem(contexts.input));
            Add(new HandleEscButtonSystem(contexts.gameInfo));
        }
    }
}
