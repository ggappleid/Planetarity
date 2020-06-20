#pragma warning disable 0649

using Entitas.Unity;
using UnityEngine;

public class RocketView : MonoBehaviour, IView
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    public Transform Transform => _transform != null ? _transform : _transform = GetComponent<Transform>();
    public Rigidbody Rigidbody => _rigidbody != null ? _rigidbody : _rigidbody = GetComponent<Rigidbody>();
    
    private EntityLink _link;
    private GameEntity LinkedEntity => (GameEntity) _link.entity;

    [SerializeField] private Transform visual;
    
    // Use DI in real application instead
    public void Init(EntityLink link)
    {
        _link = link;
        // Set initial position
        Transform.position = LinkedEntity.position.Value;
        // Set initial rotation
        Transform.LookAt(Transform.position + LinkedEntity.velocity.Value, Vector3.up);
        // Apply initial velocity
        Rigidbody.velocity = LinkedEntity.velocity.Value;
    }

    public void BeforeDestroy(bool isLoudDestroy)
    {
    }

    private void OnDestroy()
    {
        if (LinkedEntity != null)
        {
            gameObject.Unlink();
        }
    }

    private void Update()
    {
        var forwardDir = Transform.position + Rigidbody.velocity;
        Transform.LookAt(forwardDir, Vector3.up);
    }
}
