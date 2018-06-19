﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Newtonsoft.Json;

public class UiGlue : MonoBehaviour
{

	public DialogueTree model;
	
	public GameObject PhraseUi;
	public GameObject EndingUi;
	public GameObject ChoiceUi;
	public Slider DefenderPowerMeter;

	private GameState gameState;
	
	// Background image
	public Image BackgroundImage;
	
	// Phrase UI
	private Image _actorImage;
	private Image _actorNameBox;
	private Image _dialogBox;
	private Image _monologueBox;
	private Text _actorNameText;
	private Text _dialogText;
	private Text _monologueText;
	
	
	// Choice UI
	private Text _choiceHeader;
	private Text[] _choicesTexts;
	private Button _choiceDefenderButton;
	private Button[] _choicesButtons;
	
	// Ending UI
	private Text _endingText;

	// Loaded and cached actor sprites
	private Dictionary<string, Sprite> _actorSprites;

	// Loaded and cached background sprites
	private Dictionary<string, Sprite> _backgroundSprites;
	
	// Use this for initialization
	void Awake ()
	{
		gameState = Toolbox.RegisterComponent<GameState>();

		// Populate Phrase UI
		Image[] phraseUiImages = PhraseUi.GetComponentsInChildren<Image>();
		_actorImage = phraseUiImages[0];
		_actorNameBox = phraseUiImages[1];
		_actorImage.preserveAspect = true;
		_dialogBox = phraseUiImages[2];
		_monologueBox = phraseUiImages[3];
		
		Text[] phraseUiTexts = PhraseUi.GetComponentsInChildren<Text>();

		_actorNameText = phraseUiTexts[0];
		_dialogText = phraseUiTexts[1];
		_monologueText = phraseUiTexts[2];

		// Populate Choice UI
		Text[] choiceUiTexts = ChoiceUi.GetComponentsInChildren<Text>();
		Button[] choiceUiButtons = ChoiceUi.GetComponentsInChildren<Button>();
		// choiceUiTexts[0] should be our Defender button text, don't touch it
		_choiceHeader = choiceUiTexts[1];
		_choiceDefenderButton = choiceUiButtons[0];
		// The rest of the texts should be our buttons
		// FIXME: hardcoded to 3 buttons for now
		//_choicesTexts = new Text[choiceUiTexts.Length - 2];
		_choicesTexts = new Text[3];
		_choicesButtons = new Button[choiceUiButtons.Length - 1];
		for (int i = 0; i < _choicesTexts.Length; ++i)
		{
			_choicesTexts[i] = choiceUiTexts[i + 2];
			_choicesButtons[i] = choiceUiButtons[i + 1];
		}
		
		// Populate Ending UI
		Text[] endingUiTexts = EndingUi.GetComponentsInChildren<Text>();
		_endingText = endingUiTexts[0];
	}

	// We need to have Model initialized here
	void Start()
	{
		// Workaround for "clever" slider implementation
		DefenderPowerMeter.minValue = -1;
		DefenderPowerMeter.maxValue = gameState.maxPower;
		DefenderPowerMeter.value = -1;
		
		if (_actorSprites == null)
		{
			_actorSprites = new Dictionary<string, Sprite>();
			// Preload actor images
			var actorImages = model.GetUsedActorImages();
			foreach (var image in actorImages)
			{
				_actorSprites[image] = Resources.Load<Sprite>(image);
			}	
		}

		if (_backgroundSprites == null)
		{
			_backgroundSprites = new Dictionary<string, Sprite>();
			var backgroundImages = model.GetUsedBackgroundImages();
			foreach (var image in backgroundImages)
			{
				_backgroundSprites[image] = Resources.Load<Sprite>(image);
			}
		}
	}

	public void returnToHub()
	{
		model.FinalizeChapter();
	}

	private void HideAllUi()
	{
		EndingUi.SetActive(false);
		ChoiceUi.SetActive(false);
		PhraseUi.SetActive(false);
	}


	public void ShowPhraseUi(PhraseEvent pev)
	{
		if (_actorSprites == null)
		{
			Start();
		}
		HideAllUi();
		PhraseUi.SetActive(true);
		if (pev.speakerName != null)
		{
			_actorNameBox.gameObject.SetActive(true);
			_actorNameText.gameObject.SetActive(true);
			_actorNameText.text = pev.speakerName;
			_dialogBox.gameObject.SetActive(true);
			_monologueBox.gameObject.SetActive(false);
			_dialogText.text = pev.text;
		}
		else
		{
			_actorNameBox.gameObject.SetActive(false);
			_actorNameText.gameObject.SetActive(false);
			_dialogBox.gameObject.SetActive(false);
			_monologueBox.gameObject.SetActive(true);
			_monologueText.text = pev.text;
		}
		
		Sprite actorSprite;
		var actorImage = model.GetCurrentActorImage();
		if (actorImage != null && _actorSprites.TryGetValue(actorImage, out actorSprite))
		{
			_actorImage.gameObject.SetActive(true);
			_actorImage.sprite = actorSprite;
		}
		else
		{
			_actorImage.gameObject.SetActive(false);
		}
	}

	public void ShowChoiceUi(DialogueChoiceEvent cev, bool showAlternateText = false)
	{
		HideAllUi();
		ChoiceUi.SetActive(true);
		_choiceHeader.text = cev.header;
		
		// Workaround for "clever" slider implementation
		DefenderPowerMeter.minValue = 0;
		DefenderPowerMeter.maxValue = gameState.maxPower;
		DefenderPowerMeter.value = gameState.currentPower;
		
		// Should only show defender button if energy is high enough
		// TODO: move to game logic
		var shouldShowDefender = !showAlternateText;
		_choiceDefenderButton.gameObject.SetActive(shouldShowDefender);
		_choiceDefenderButton.onClick.RemoveAllListeners();
		if (gameState.currentPower >= cev.defenderCost)
		{
			_choiceDefenderButton.image.color = new Color(147, 194, 248);
			_choiceDefenderButton.onClick.AddListener(delegate { OnDefender(); });
		}
		else
		{
			_choiceDefenderButton.image.color = Color.grey;
		}
		
		for (int i = 0; i < _choicesTexts.Length; ++i)
		{
			// Only show available choices
			if (i < cev.choices.Length)
			{
				_choicesButtons[i].gameObject.SetActive(true);
				if (!showAlternateText)
				{
					_choicesTexts[i].color = Color.black;
					_choicesTexts[i].text = cev.choices[i].choiceText;
				}
				else
				{
					if (cev.choices[i].defenderColor != null)
					{
						Color color;
						if (ColorUtility.TryParseHtmlString(cev.choices[i].defenderColor, out color))
						{
							_choicesButtons[i].image.color = color;
						}	
					}
					
					if (cev.choices[i].defenderText != null)
					{
						_choicesTexts[i].text = cev.choices[i].defenderText;
					}
					else
					{
						_choicesTexts[i].text = cev.choices[i].choiceText;
					}
					
				}
			}
			else
			{
				_choicesButtons[i].gameObject.SetActive(false);
			}
		}
	}

	public void ShowEndingUi(FinalDialogueEvent fev)
	{
		HideAllUi();
		EndingUi.SetActive(true);
		_endingText.text = fev.text;
	}

	// Choice buttons handler (called by Unity)
	public void OnChoice(int choice)
	{
		model.OnChoice(choice);
	}
	
	// Forward button handler (called by Unity)
	public void OnForward()
	{
		model.OnForward();
	}

	// Defender button handler (called by Unity)
	public void OnDefender()
	{
		model.OnDefender();
	}
	
	// Change background as requested
	public void ChangeBackground(BackgroundChangeEvent bev)
	{
		var backgroundImageName = model.GetBackgroundImageByName(bev.backgroundName);
		BackgroundImage.sprite = _backgroundSprites[backgroundImageName];
	}
	
	// Show gender choice UI
	public void ShowGenderUi()
	{
		// TODO
	}

	public void ShowPlayerUi(PlayerPhraseEvent pev)
	{
		// TODO
	}
	
}
