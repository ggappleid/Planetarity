using UnityEngine;

namespace Entitas.Features.Input
{
    public class HandleMouseClickExecuteSystem : IExecuteSystem, ICleanupSystem
    {
        private readonly InputContext _input;
        
        public HandleMouseClickExecuteSystem(InputContext input)
        {
            _input = input;
        }
        
        public void Execute()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _input.ReplaceScreenClick(UnityEngine.Input.mousePosition);
            }
        }

        public void Cleanup()
        {
            if (!UnityEngine.Input.GetMouseButtonUp(0))
            {
                return;
            }
            
            if (_input.hasScreenClick)
            {
                _input.RemoveScreenClick();
            }
        }
    }
}
