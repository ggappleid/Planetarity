#pragma warning disable 0649

using TMPro;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float _maxValue = 30f;

    [SerializeField] private RectTransform _barRect;
    [SerializeField] private TextMeshProUGUI _valueLbl;
    
    public void SetMaxValue(float maxValue)
    {
        _maxValue = maxValue;
    }

    public void SetValue(float value)
    {
        if (_maxValue <= 0f)
        {
            return;
        }
        
        var scale = Mathf.Clamp01(value / _maxValue);
        _valueLbl.text = Mathf.CeilToInt(value).ToString();
        _barRect.localScale = new Vector3(scale, 1f, 1f);
    }
}
