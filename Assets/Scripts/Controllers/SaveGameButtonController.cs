using UnityEngine;
using UnityEngine.UI;

public class SaveGameButtonController : MonoBehaviour
{
    private Button _button;
    
    void Awake()
    {
        _button = GetComponentInChildren<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
    
    void OnButtonClick()
    {
        Contexts.sharedInstance.gameInfo.isSaveGame = true;
    }
}
