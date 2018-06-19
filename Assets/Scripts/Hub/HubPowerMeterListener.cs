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
	
	/**
	 * Update the text value according to the slider 
	 */
	public void OnValueChanged(Slider slider)
	{
		if (_text == null)
		{
			_text = GetComponent<Text>();
		}
		_text.text = slider.value + "/" + slider.maxValue;
	}
}
