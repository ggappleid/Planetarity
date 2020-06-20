#pragma warning disable 0649

using Entitas;
using UnityEngine;

public class StartButtonView : MonoBehaviour
{
    private GameContext _game;
    private IGroup<GameEntity> _gameTimerGroup;
    
    private void Awake()
    {
        _game = Contexts.sharedInstance.game;
        // _gameTimerGroup = _game.GetGroup(GameMatcher.GameTimer);
    }

    private void OnEnable()
    {
        // Very simplified approach of getting data from Entitas
        _gameTimerGroup.OnEntityAdded += OnGameTimerAdded;
    }

    private void OnDisable()
    {
        _gameTimerGroup.OnEntityAdded -= OnGameTimerAdded;
    }

    private void OnGameTimerAdded(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        this.gameObject.SetActive(false);
    }
}
