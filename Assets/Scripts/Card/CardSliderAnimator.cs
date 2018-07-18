using UnityEngine;
using UnityEngine.UI;

public class CardSliderAnimator
{
    private Slider _slider;
    private Image _sliderFill;
    private float _startTime;
    private float _endTime;
    private float _startValue;
    private float _targetValue;
    private Color _deltaColor;

    public CardSliderAnimator(Slider animSlider, float targetValue, float duration)
    {
        _slider = animSlider;
        _sliderFill = _slider.GetComponentsInChildren<Image>()[1];
        _startTime = Time.time;
        _endTime = _startTime + duration;
        _startValue = animSlider.value;
        _targetValue = targetValue;
        if (_targetValue - _startValue > 0.0001f)
        {
            _deltaColor = Color.green;
        }
        else if (_targetValue - _startValue < -0.0001f)
        {
            _deltaColor = Color.red;
        }
        else
        {
            // No changes required
            _deltaColor = Color.white;
        }
    }

    /**
     * "Ease-in" animation function.
     * Provides smooth declining velocity.
     * Parameter t is clamped to [0.0; 1.0]
     * Output is 0.0 for t=0.0, 1.0 for t=1.0
     */
    private float funcEaseIn(float t)
    {
        float ct = Mathf.Clamp01(t);
        return -(ct - 1) * (ct - 1) + 1;
    }

    /**
     * Update the slider value and color 
     */
    public void Update()
    {
        float timeDelta = Time.time - _startTime;
        float hTime = timeDelta / (_endTime - _startTime);
        float t = Mathf.Clamp01(funcEaseIn(hTime));
        _slider.value = _startValue * (1 - t) + _targetValue * t;
        _sliderFill.color = Color.Lerp(_deltaColor, Color.white, t);
    }

    public bool IsEnded()
    {
        return Time.time > _endTime;
    }
    
}
