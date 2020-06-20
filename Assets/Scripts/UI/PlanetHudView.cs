#pragma warning disable 0649

using Entitas.Unity;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHudView : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image progressBar;
    [SerializeField] private Image cooldownBackground;
    [SerializeField] private Image cooldown;
    
    private EntityLink _link;
    private GameEntity LinkedEntity => (GameEntity) _link.entity;
    
    private Color _playerColor = Color.green;
    private Color _botColor = Color.red;

    private Camera _mainCamera;
    private Canvas _canvas;
    
    private RectTransform _rectTransform;
    private RectTransform RectTransform => _rectTransform != null ? _rectTransform : _rectTransform = GetComponent<RectTransform>();

    private float CooldownValue
    {
        get
        {
            var cooldownTime = LinkedEntity.cannon.CooldownTime;
            var timeLeft = LinkedEntity.hasCooldownTimer ? LinkedEntity.cooldownTimer.Value : 0f;
            var result = (cooldownTime - timeLeft) / cooldownTime;
            return Mathf.Clamp(result, 0f, 1000f);
        }
    }

    public void Init(EntityLink link)
    {
        _link = link;

        var playerE = Contexts.sharedInstance.game.GetPlayerEntity();
        progressBar.color = LinkedEntity == Contexts.sharedInstance.game.GetPlanetEntity(playerE)
            ? _playerColor 
            : _botColor;
        
        UpdateCooldown();
    }

    private void Awake()
    {
        _mainCamera = SolarSystemView.Instance.mainCamera;
        _canvas = SolarSystemView.Instance.uiCanvas;
    }
    
    private void OnDestroy()
    {
        if (LinkedEntity != null)
        {
            gameObject.Unlink();
        }
    }
    
    public void Update()
    {
        healthBar.value = LinkedEntity.health.Value / Consts.PlanetHealth;
        
        UpdateCooldown();
        
        var position = _canvas.WorldToCanvasPosition(LinkedEntity.view.Value.transform.position, _mainCamera);
        RectTransform.anchoredPosition = position;
    }

    private void UpdateCooldown()
    {
        cooldownBackground.fillAmount = CooldownValue;
        cooldown.fillAmount = CooldownValue;
    }
}
