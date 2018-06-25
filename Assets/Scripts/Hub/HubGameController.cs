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
	private Dictionary<int, CardGameDescription> _cards;

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
				
		var cardJson = Resources.Load<TextAsset>("Cards/manifest");
		var cardList = JsonConvert.DeserializeObject<List<CardGameDescription>>(cardJson.text);
		_cards = new Dictionary<int, CardGameDescription>();
		foreach (var card in cardList)
		{
			if (_cards.ContainsKey(card.Id))
			{
				Debug.LogError("Card game ID " + card.Id + " already present!");
			}

			_cards[card.Id] = card;
		}

		_gameState.ChapterResource = _chapters[_gameState.currentScene].ScenarioFilename;
		_gameState.CardScriptResource = _cards[_gameState.CurrentCardGameId].CardScriptFilename;
		
		// Advance chapter if possible
		if (_gameState.NextChapterId != -1)
		{
			_gameState.currentScene = _gameState.NextChapterId;
			_gameState.ChapterResource = _chapters[_gameState.currentScene].ScenarioFilename;
		}

		if (_gameState.NextCardGameId != -1)
		{
			_gameState.CurrentCardGameId = _gameState.NextCardGameId;
			_gameState.CardScriptResource = _cards[_gameState.CurrentCardGameId].CardScriptFilename;
		}

		_gameState.NextChapterId = _chapters[_gameState.currentScene].NextChapterId;
		_gameState.NextCardGameId = _cards[_gameState.CurrentCardGameId].NextCardGameId;
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
		}
		else
		{
			UiGlue.SetStoryButtonDisabled();
		}

		if (_gameState.currentExperience >= _cards[_gameState.CurrentCardGameId].MinXpRequired)
		{
			UiGlue.SetCardButtonEnabled();
		}
		else
		{
			UiGlue.SetCardButtonDisabled();
		}
		
		UiGlue.SetStoryModeText(_chapters[_gameState.currentScene].ChapterName);
	}
}
