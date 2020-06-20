#pragma warning disable 0649

using Entitas;
using UnityEngine;

public class GameTimerView : MonoBehaviour
{
    private GameContext _game;
    private IGroup<GameEntity> _timerGroup;

    [SerializeField] private ProgressBar _progressBar;
    private GameObject _progressBarGo;
    
    private void Awake()
    {
        _game = Contexts.sharedInstance.game;
        // _timerGroup = _game.GetGroup(GameMatcher.GameTimer);
        // _progressBar.SetMaxValue(Consts.DefaultRoundTime);
        _progressBarGo = _progressBar.gameObject;
    }
    
    private void Start()
    {
        _progressBarGo.SetActive(false);
    }

    private void OnEnable()
    {
        // Very simplified approach of getting data from Entitas
        _timerGroup.OnEntityAdded += GameTimerAdded;
        _timerGroup.OnEntityRemoved += GameTimerRemoved;
    }

    private void OnDisable()
    {
        _timerGroup.OnEntityAdded -= GameTimerAdded;
        _timerGroup.OnEntityRemoved -= GameTimerRemoved;
    }
    
    private void GameTimerRemoved(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        _progressBarGo.SetActive(false);
    }

    private void GameTimerAdded(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        // _progressBar.SetValue(entity.gameTimer.TimeLeft);
        // _progressBarGo.SetActive(entity.gameTimer.TimeLeft > 0f);
    }
}
