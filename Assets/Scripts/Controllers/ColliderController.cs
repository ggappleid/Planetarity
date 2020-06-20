using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private GameContext _game;
    
    private void Awake()
    {
        _game = Contexts.sharedInstance.game;
    }

    private void OnCollisionEnter(Collision other)
    {
        _game
            .CreateEntity()
            .ReplaceCollision(gameObject, other.gameObject);
    }
}
