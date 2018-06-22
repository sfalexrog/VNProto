﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUiGlue : MonoBehaviour
{
	public CardController Model;
	
	private GameObject _purposePanel;
	private GameObject _answerPanel;
	private GameObject _parametersPanel;
	private GameObject _leftAnswer;
	private GameObject _rightAnswer;

	private Button _leftAnswerButton;
	private Button _rightAnswerButton;
	private Button _cancelAnswerButton;

	private Text _leftAnswerText;
	private Text _rightAnswerText;

	private Text _nameText;
	private Text _questionText;

	private Text _goalText;
	
	private Image _actorImage;

	private Image _familyImage;
	private Image _friendImage;
	private Image _coupleImage;
	private Image _classImage;

	private Slider _familySlider;
	private Slider _friendSlider;
	private Slider _coupleSlider;
	private Slider _classSlider;

	private string _leftButtonStoredText;
	private string _rightButtonStoredText;

	private float[] _leftResults;
	private float[] _rightResults;

	private CardSliderAnimator[] _animations;
	
	// Bind to scene objects
	void Awake()
	{
		
		_purposePanel = GameObject.Find("Purpose_Panel");
		_answerPanel = GameObject.Find("Answer_Panel");
		_parametersPanel = GameObject.Find("Parameters_Panel");
		_leftAnswer = GameObject.Find("Left answer");
		_rightAnswer = GameObject.Find("Right answer");

		_leftAnswerButton = _leftAnswer.GetComponent<Button>();
		_rightAnswerButton = _rightAnswer.GetComponent<Button>();
		_cancelAnswerButton = GameObject.Find("Cancel Choice Area").GetComponent<Button>();
		_cancelAnswerButton.onClick.AddListener(delegate { CancelOutcome(); });

		_leftAnswerText = _leftAnswer.GetComponentInChildren<Text>();
		_rightAnswerText = _rightAnswer.GetComponentInChildren<Text>();

		_goalText = GameObject.Find("Goal Counter Text").GetComponent<Text>();
		
		_leftAnswerText.text = "Left answer";
		_rightAnswerText.text = "Right answer";

		_nameText = GameObject.Find("Name Text").GetComponent<Text>();
		_questionText = GameObject.Find("Question Text").GetComponent<Text>();

		_actorImage = GameObject.Find("Actor Image").GetComponent<Image>();

		_familyImage = GameObject.Find("Family Image").GetComponent<Image>();
		_friendImage = GameObject.Find("Friend Image").GetComponent<Image>();
		_coupleImage = GameObject.Find("Couple Image").GetComponent<Image>();
		_classImage = GameObject.Find("Class Image").GetComponent<Image>();

		_familySlider = GameObject.Find("Family_Slider").GetComponent<Slider>();
		_friendSlider = GameObject.Find("Friend_Slider").GetComponent<Slider>();
		_coupleSlider = GameObject.Find("Girlfriend_Slider").GetComponent<Slider>();
		_classSlider = GameObject.Find("Class_Slider").GetComponent<Slider>();
		
		_animations = new CardSliderAnimator[4];
		
		ResetUi();
		ResetListeners();
	}

	// Load a card and display it
	private void Start()
	{
		Advance();
	}

	private void Advance()
	{
		_goalText.text = Model.GetCardsRemaining().ToString();
		Card card = Model.GetNextCard();		
		
		_leftResults = Model.GetRelationsChange(0);
		_rightResults = Model.GetRelationsChange(1);
		DisplayRelations();
		DisplayCard(card);
	}

	private void DisplayCurrentCard()
	{
		DisplayCard(Model.CurrentCard);
	}

	private void DisplayCard(Card card)
	{
		
		_leftAnswerText.text = card.LeftButtonText;
		_rightAnswerText.text = card.RightButtonText;
		
		_questionText.text = card.Question;
		// TODO: display actor image in card
		_nameText.text = card.Actor;
		
		// Store button text, just in case
		_leftButtonStoredText = card.LeftButtonText;
		_rightButtonStoredText = card.RightButtonText;
	}

	private void DisplayRelations()
	{
		// TODO: convert to animations
		_animations[0] = new CardSliderAnimator(_familySlider, Model.GetCurrentRelations(Relation.FAMILY), 1.0f);
		_animations[1] = new CardSliderAnimator(_friendSlider, Model.GetCurrentRelations(Relation.FRIENDS), 1.0f);
		_animations[2] = new CardSliderAnimator(_coupleSlider, Model.GetCurrentRelations(Relation.COUPLE), 1.0f);
		_animations[3] = new CardSliderAnimator(_classSlider, Model.GetCurrentRelations(Relation.CLASS), 1.0f);
	}

	private void ResetUi()
	{
		_leftAnswerButton.image.color = Color.white;
		_rightAnswerButton.image.color = Color.white;
		_cancelAnswerButton.gameObject.SetActive(false);
		
		_familyImage.gameObject.SetActive(false);
		_friendImage.gameObject.SetActive(false);
		_coupleImage.gameObject.SetActive(false);
		_classImage.gameObject.SetActive(false);

		if (_leftButtonStoredText != null)
		{
			_leftAnswerText.text = _leftButtonStoredText;
		}

		if (_rightButtonStoredText != null)
		{
			_rightAnswerText.text = _rightButtonStoredText;
		}
		
	}

	private void ResetListeners()
	{
		_leftAnswerButton.onClick.RemoveAllListeners();
		_rightAnswerButton.onClick.RemoveAllListeners();
		
		_leftAnswerButton.onClick.AddListener(delegate { PreviewOutcome(_leftAnswerButton, _rightAnswerButton); });
		_rightAnswerButton.onClick.AddListener(delegate { PreviewOutcome(_rightAnswerButton, _leftAnswerButton); });
	}


	private void PreviewOutcome(Button outcomeButton, Button otherButton)
	{
		outcomeButton.image.color = Color.yellow;
		outcomeButton.onClick.RemoveAllListeners();
		outcomeButton.onClick.AddListener(delegate { ApplyOutcome(outcomeButton); });

		_leftButtonStoredText = _leftAnswerText.text;
		_rightButtonStoredText = _rightAnswerText.text;

		outcomeButton.GetComponentInChildren<Text>().text = "Ответить";
		
		otherButton.onClick.RemoveAllListeners();
		otherButton.onClick.AddListener(delegate
		{
			ResetUi();
			PreviewOutcome(otherButton, outcomeButton);
		});
		
		_cancelAnswerButton.gameObject.SetActive(true);
		
		_familyImage.gameObject.SetActive(true);
		_friendImage.gameObject.SetActive(true);
		_coupleImage.gameObject.SetActive(true);
		_classImage.gameObject.SetActive(true);
		// TODO: add predictions from model
		float[] relationChange = null;
		if (outcomeButton == _leftAnswerButton)
		{
			relationChange = _leftResults;
		}
		else if (outcomeButton == _rightAnswerButton)
		{
			relationChange = _rightResults;
		}
		else
		{
			Debug.LogError("Unexpected outcome button");
		}

		var familyScale = relationChange[(int) Relation.FAMILY];
		_familyImage.gameObject.transform.localScale = new Vector3(familyScale, familyScale, familyScale);
		var friendScale = relationChange[(int) Relation.FRIENDS];
		_friendImage.gameObject.transform.localScale = new Vector3(friendScale, friendScale, friendScale);
		var coupleScale = relationChange[(int) Relation.COUPLE];
		_familyImage.gameObject.transform.localScale = new Vector3(coupleScale, coupleScale, coupleScale);
		var classScale = relationChange[(int) Relation.CLASS];
		_classImage.gameObject.transform.localScale = new Vector3(classScale, classScale, classScale);
	}

	private void CancelOutcome()
	{
		ResetUi();
		ResetListeners();
	}

	private void ApplyOutcome(Button outcomeButton)
	{
		if (outcomeButton == _leftAnswerButton)
		{
			Model.ApplyChange(0);
		}
		else if (outcomeButton == _rightAnswerButton)
		{
			Model.ApplyChange(1);
		}
		Advance();
		ResetUi();
		ResetListeners();
	}
	
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < _animations.Length; ++i)
		{
			if (_animations[i] != null)
			{
				_animations[i].Update();
				if (_animations[i].IsEnded())
				{
					_animations[i] = null;
				}
			}
		}
	}
}
