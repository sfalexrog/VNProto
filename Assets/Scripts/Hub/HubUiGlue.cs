using System.Collections;
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
	public Image ModeDisabledPopup;
	public Text ModeDisabledDescription;
	
	public void StartNextLevel()
	{
		// Prevent card game from advancing
		var gameState = Toolbox.RegisterComponent<GameState>();
		gameState.NextCardGameId = -1;
		SceneManager.LoadScene("Scenes/VNInkyScene");
	}

    public void StartCardGame()
    {
	    // Prevent chapter from advancing
	    var gameState = Toolbox.RegisterComponent<GameState>();
	    gameState.NextChapterId = -1;
        SceneManager.LoadScene("Scenes/CardGame");
    }

	public void ShowStoryModeDisabledPopup()
	{
		// TODO
		Debug.LogWarning("Story mode is disabled at the moment");
		ModeDisabledPopup.gameObject.SetActive(true);
		ModeDisabledDescription.text = "Продолжение истории в данный момент недоступно. Идите в Город, чтобы получить больше опыта.";
	}

	public void ShowCardGameDisabledPopup()
	{
		// TODO
		Debug.LogWarning("Card game is disabled at the moment");
		ModeDisabledPopup.gameObject.SetActive(true);
		ModeDisabledDescription.text = "Режим игры в данный момент недоступен. Пора продолжать историю!";
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

	public void DismissModeDisabledDialog()
	{
		ModeDisabledPopup.gameObject.SetActive(false);
	}
	
}
