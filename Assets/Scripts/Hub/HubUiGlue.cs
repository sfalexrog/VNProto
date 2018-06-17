﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubUiGlue : MonoBehaviour
{
	public Slider PowerSlider;
	public Text LevelText;
	public Button StoryModeButton;
	public Button CardGameButton;
	public Image StoryModeLockedImage;
	
	public void StartNextLevel()
	{
		SceneManager.LoadScene("Scenes/VNScene");
	}

    public void StartCardGame()
    {
        SceneManager.LoadScene("Scenes/CardGame");
    }

	public void ShowStoryModeDisabledPopup()
	{
		// TODO
		Debug.LogWarning("Story mode is disabled at the moment");
	}

	public void ShowCardGameDisabledPopup()
	{
		// TODO
		Debug.LogWarning("Card game is disabled at the moment");
	}

	public void DisableAllButtons()
	{
		StoryModeButton.onClick.RemoveAllListeners();
		CardGameButton.onClick.RemoveAllListeners();
	}

	public void SetStoryButtonEnabled()
	{
		StoryModeButton.onClick.RemoveAllListeners();
		StoryModeButton.onClick.AddListener(delegate { StartNextLevel(); });
		StoryModeButton.image.color = Color.white;
		StoryModeLockedImage.gameObject.SetActive(false);
	}

	public void SetStoryButtonDisabled()
	{
		StoryModeButton.onClick.RemoveAllListeners();
		StoryModeButton.onClick.AddListener(delegate { ShowStoryModeDisabledPopup(); });
		StoryModeButton.image.color = Color.gray;
		StoryModeLockedImage.gameObject.SetActive(true);
	}

	public void SetCardButtonEnabled()
	{
		CardGameButton.onClick.RemoveAllListeners();
		CardGameButton.onClick.AddListener(delegate { StartCardGame(); });
	}

	public void SetCardButtonDisabled()
	{
		CardGameButton.onClick.RemoveAllListeners();
		CardGameButton.onClick.AddListener(delegate { ShowCardGameDisabledPopup(); });
	}

	public void SetPowerMeterMaxValue(int maxValue)
	{
		PowerSlider.maxValue = maxValue;
	}

	public void SetPowerMeterValue(int value)
	{
		PowerSlider.value = value;
	}

	public void SetStoryModeText(string text)
	{
		LevelText.text = text;
	}
}
