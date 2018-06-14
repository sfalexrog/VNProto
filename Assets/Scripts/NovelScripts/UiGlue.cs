using System;
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

	private GameState gameState;
	
	// Background image
	public Image BackgroundImage;
	
	// Phrase UI
	private Image _actorImage;
	private Image _actorNameBox;
	private Text _actorNameText;
	private Text _dialogText;
	
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
		
		Text[] phraseUiTexts = PhraseUi.GetComponentsInChildren<Text>();

		_actorNameText = phraseUiTexts[0];
		_dialogText = phraseUiTexts[1];

		// Populate Choice UI
		Text[] choiceUiTexts = ChoiceUi.GetComponentsInChildren<Text>();
		Button[] choiceUiButtons = ChoiceUi.GetComponentsInChildren<Button>();
		// choiceUiTexts[0] should be our Defender button text, don't touch it
		_choiceHeader = choiceUiTexts[1];
		_choiceDefenderButton = choiceUiButtons[0];
		// The rest of the texts should be our buttons
		_choicesTexts = new Text[choiceUiTexts.Length - 2];
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


	public void ShowPhraseUi(PhraseEvent pev)
	{
		if (_actorSprites == null)
		{
			Start();
		}
		EndingUi.SetActive(false);
		ChoiceUi.SetActive(false);
		PhraseUi.SetActive(true);
		if (pev.speakerName != null)
		{
			_actorNameBox.gameObject.SetActive(true);
			_actorNameText.gameObject.SetActive(true);
			_actorNameText.text = pev.speakerName;	
		}
		else
		{
			_actorNameBox.gameObject.SetActive(false);
			_actorNameText.gameObject.SetActive(false);
		}
		
		_dialogText.text = pev.text;

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
		EndingUi.SetActive(false);
		ChoiceUi.SetActive(true);
		PhraseUi.SetActive(false);
		_choiceHeader.text = cev.header;
		
		// Should only show defender button if energy is high enough
		var shouldShowDefender = !showAlternateText && (gameState.currentPower >= cev.defenderCost);
		_choiceDefenderButton.gameObject.SetActive(shouldShowDefender);
		
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
					_choicesTexts[i].color = Color.green;
					_choicesTexts[i].text = cev.choices[i].defenderText;
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
		EndingUi.SetActive(true);
		ChoiceUi.SetActive(false);
		PhraseUi.SetActive(false);

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
}
