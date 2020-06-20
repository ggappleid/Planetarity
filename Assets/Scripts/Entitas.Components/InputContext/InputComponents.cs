using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Input, Unique]
public class ScreenClickComponent : IComponent
{
    public Vector2 Value;
}

