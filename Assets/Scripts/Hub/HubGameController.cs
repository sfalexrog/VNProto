using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class HubGameController : MonoBehaviour
{
	public HubUiGlue UiGlue;
	public bool RestoreDefenderOnRestart;

	private GameState _gameState;

	private Dictionary<int, ChapterDescription> _chapters;

	void Awake()
	{
		_gameState = Toolbox.RegisterComponent<GameState>();
		var chapterJson = Resources.Load<TextAsset>("Scenarios/manifest");
		var chapterList = JsonConvert.DeserializeObject<List<ChapterDescription>>(chapterJson.text);
		_chapters = new Dictionary<int, ChapterDescription>();
		foreach (var chapter in chapterList)
		{
			if (_chapters.ContainsKey(chapter.Id))
			{
				Debug.LogError("Chapter ID " + chapter.Id + " already present!");
			}

			_chapters[chapter.Id] = chapter;
		}
		
		// Advance chapter if possible
		if (_gameState.NextChapterId != -1)
		{
			_gameState.currentScene = _gameState.NextChapterId;
			_gameState.ChapterResource = _chapters[_gameState.currentScene].ScenarioFilename;
		}

		_gameState.NextChapterId = _chapters[_gameState.currentScene].NextChapterId;
	}

	// Use this for initialization
	void Start ()
	{
		if (RestoreDefenderOnRestart)
		{
			_gameState.currentPower = _gameState.maxPower;
		}
		UiGlue.DisableAllButtons();
		UiGlue.SetPowerMeterMaxValue(_gameState.maxPower);
		UiGlue.SetPowerMeterValue(_gameState.currentPower);
		
		// TODO: additional criteria for card game button?
		if (_gameState.currentExperience >= _chapters[_gameState.currentScene].MinXpRequired)
		{
			UiGlue.SetStoryButtonEnabled();
			UiGlue.SetCardButtonDisabled();
		}
		else
		{
			UiGlue.SetStoryButtonDisabled();
			UiGlue.SetCardButtonEnabled();
		}
		
		UiGlue.SetStoryModeText(_chapters[_gameState.currentScene].ChapterName);
	}
}
