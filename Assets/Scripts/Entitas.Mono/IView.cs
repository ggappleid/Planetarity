
using Entitas.Unity;
using UnityEngine;

public interface IView
{
    Rigidbody Rigidbody { get; }
    Transform Transform { get; }
    void Init(EntityLink link);
    void BeforeDestroy(bool isLoud);
}
