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

	public Image[] characterImages;

	private GameState gameState;
	
	// Phrase UI
	private Text _actorNameText;
	private Text _dialogText;
	
	// Choice UI
	private Text _choiceHeader;
	private Text[] _choicesTexts;
	private Button _choiceDefenderButton;
	private Button[] _choicesButtons;
	
	// Ending UI
	private Text _endingText;

	// Use this for initialization
	void Awake ()
	{
		gameState = Toolbox.RegisterComponent<GameState>();

		// Populate Phrase UI
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
	
	// Update is called once per frame
	void Update () {
		
	}

	public void returnToHub()
	{
		gameState.currentScene += 1;
		SceneManager.LoadScene(0);
	}


	public void ShowPhraseUi(PhraseEvent pev)
	{
		EndingUi.SetActive(false);
		ChoiceUi.SetActive(false);
		PhraseUi.SetActive(true);
		_actorNameText.text = pev.speakerName;
		_dialogText.text = pev.text;
	}

	public void ShowChoiceUi(DialogueChoiceEvent cev, bool showAlternateText = false)
	{
		EndingUi.SetActive(false);
		ChoiceUi.SetActive(true);
		PhraseUi.SetActive(false);
		_choiceHeader.text = cev.header;
		_choiceDefenderButton.gameObject.SetActive(!showAlternateText);
		
		for (int i = 0; i < _choicesTexts.Length; ++i)
		{
			// Only show available choices
			if (i < cev.choices.Length)
			{
				_choicesButtons[i].gameObject.SetActive(true);
				if (!showAlternateText)
				{
					_choicesTexts[i].text = cev.choices[i].choiceText;
				}
				else
				{
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
}
