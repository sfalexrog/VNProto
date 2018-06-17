using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubPowerMeterListener : MonoBehaviour
{

	/**
	 * Text that will be changed to reflect slider changes
	 */
	private Text _text;
	
	private void Awake()
	{
		_text = GetComponent<Text>();
	}
	
	/**
	 * Update the text value according to the slider 
	 */
	public void OnValueChanged(Slider slider)
	{
		_text.text = slider.value + "/" + slider.maxValue;
	}
}
