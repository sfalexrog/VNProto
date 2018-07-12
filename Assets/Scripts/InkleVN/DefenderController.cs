using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderController : MonoBehaviour {

    [Header("Defender Slider Parts")]
    public Slider DefenderSlider;
    public Text DefenderPowerText;

    [Header("Animation parameters")]
    public AnimationCurve TransitionCurve;
    public float TransitionDuration;


    private class SliderAnimation
    {
        public float TimeStart;
        public float TimeEnd;

        public float StartValue;
        public float EndValue;

        public AnimationCurve Curve;

        public Slider TargetSlider;

        public bool Done;

        public SliderAnimation(Slider targetSlider, float endValue, float timeStart, float duration, AnimationCurve curve)
        {
            TargetSlider = targetSlider;
            TimeStart = timeStart;
            TimeEnd = timeStart + duration;
            StartValue = targetSlider.value;
            EndValue = endValue;
            Curve = curve;
            Done = false;
        }

        public void Animate()
        {
            var t = Time.time;
            var ht = Mathf.Clamp01((t - TimeStart) / (TimeEnd - TimeStart));
            var c = Curve.Evaluate(ht);
            TargetSlider.value = c * EndValue + (1 - c) * StartValue;
            if (t > TimeEnd)
            {
                Done = true;
            }
        }

    }

    private SliderAnimation _currentAnimation;

    private int _maxValue;
    private int _currentValue;
    
    public int MaxValue
    {
        get { return _maxValue; }
        set { _maxValue = value; UpdateControl(); }
    }

    public int CurrentValue
    {
        get { return _currentValue; }
        set { _currentValue = value; UpdateControl(); }
    }

    /**
     * Updates the defender text according to current values
     */
    public void UpdateControl(bool immediate = false)
    {
        DefenderPowerText.text = $"{CurrentValue}/{MaxValue}";
        DefenderSlider.maxValue = _maxValue;
        if (immediate)
        {
            DefenderSlider.value = _currentValue;
        }
        else
        {
            _currentAnimation = new SliderAnimation(DefenderSlider, _currentValue, Time.time, TransitionDuration, TransitionCurve);
        }
    }

	void Start ()
    {
        _maxValue = 1;
        _currentValue = 0;
        UpdateControl(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (_currentAnimation != null)
        {
            _currentAnimation.Animate();
            if (_currentAnimation.Done)
            {
                _currentAnimation = null;
            }
        }
	}
}
