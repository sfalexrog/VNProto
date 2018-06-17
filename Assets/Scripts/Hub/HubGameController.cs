using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubGameController : MonoBehaviour
{

	public HubUiGlue UiGlue;

	private GameState _gameState;

	void Awake()
	{
		_gameState = Toolbox.RegisterComponent<GameState>();
	}

	// Use this for initialization
	void Start ()
	{
		UiGlue.DisableAllButtons();
		UiGlue.SetPowerMeterMaxValue(_gameState.maxPower);
		UiGlue.SetPowerMeterValue(_gameState.currentPower);
		
		// TODO: Load scene manifest and check for required XP to continue
		// TODO: additional criteria for card game button?
		if (_gameState.currentExperience >= 0)
		{
			UiGlue.SetStoryButtonEnabled();
			UiGlue.SetCardButtonDisabled();
		}
		else
		{
			UiGlue.SetStoryButtonDisabled();
			UiGlue.SetCardButtonEnabled();
		}
		
		UiGlue.SetStoryModeText("Глава " + _gameState.currentScene);
	}
}
