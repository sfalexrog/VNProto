using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubUiGlue : MonoBehaviour
{
	public Slider PowerSlider;
	public Text PowerMeter;
	public Text LevelText;
	public Button StoryModeButton;

	private GameState _gameState;
	

	// Use this for initialization
	void Start ()
	{
		_gameState = Toolbox.RegisterComponent<GameState>();
		PowerMeter.text = "Current power: " + _gameState.currentPower;
		LevelText.text = "Глава  " + _gameState.currentScene;
		PowerSlider.maxValue = _gameState.maxPower;
		PowerSlider.value = _gameState.currentPower;
	}

	public void StartNextLevel()
	{
		SceneManager.LoadScene("Scenes/VNScene");
	}

    public void StartCardGame()
    {
        SceneManager.LoadScene("Scenes/CardGame");
    }
}
