#pragma warning disable 0649

using Entitas;
using UnityEngine;

public class PausePanelView : MonoBehaviour
{
    [SerializeField] private GameObject content;
    
    private GameInfoContext _gameInfo;
    private IGroup<GameInfoEntity> _currStateGroup;
    
    private void Awake()
    {
        _gameInfo = Contexts.sharedInstance.gameInfo;
        _currStateGroup = _gameInfo.GetGroup(GameInfoMatcher.CurrentState);
    }

    private void Start()
    {
        SetActiveContent(false);
    }

    private void OnEnable()
    {
        // Extremely simplified approach of getting data from Entitas
        _currStateGroup.OnEntityAdded += OnCurrStateAdded;
    }

    private void OnDisable()
    {
        _currStateGroup.OnEntityAdded -= OnCurrStateAdded;
    }

    private void OnCurrStateAdded(IGroup<GameInfoEntity> @group, GameInfoEntity entity, int index, IComponent component)
    {
        var isInMenu = _gameInfo.currentState.Value == GameState.Menu;
        var isGamePlaying = !_gameInfo.hasGameEnded;
        var isGameWasStarted = _gameInfo.hasGameStart;
        
        SetActiveContent(isInMenu && isGamePlaying && isGameWasStarted);
    }

    private void SetActiveContent(bool isActive)
    {
        content.SetActive(isActive);
    }
}
