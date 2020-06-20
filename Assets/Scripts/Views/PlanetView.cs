using Entitas.Unity;
using UnityEngine;

public class PlanetView : MonoBehaviour, IView
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    
    public Transform Transform => 
        _transform != null 
            ? _transform 
            : _transform = GetComponent<Transform>();
    
    public Rigidbody Rigidbody => 
        _rigidbody != null 
            ? _rigidbody 
            : _rigidbody = GetComponent<Rigidbody>();
    
    private MeshRenderer MeshRenderer => 
        _meshRenderer != null 
            ? _meshRenderer 
            : _meshRenderer = GetComponentInChildren<MeshRenderer>();
    
    private EntityLink _link;
    private GameEntity LinkedEntity => (GameEntity) _link.entity;

    // Use DI in real application instead
    public void Init(EntityLink link)
    {
        _link = link;
        name = LinkedEntity.planet.Name;
        MeshRenderer.material = SolarSystemView.Instance.GetPlanetMaterial(name);
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
        if (LinkedEntity.hasPosition)
        {
            Transform.position = LinkedEntity.position.Value;
        }
    }
}
