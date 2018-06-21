using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUiGlue : MonoBehaviour
{
	

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

	private Image _actorImage;

	private Image _familyImage;
	private Image _friendImage;
	private Image _coupleImage;
	private Image _classImage;
	
	
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
		
		_leftAnswerText.text = "Left answer";
		_rightAnswerText.text = "Right answer";

		_nameText = GameObject.Find("Name Text").GetComponent<Text>();
		_questionText = GameObject.Find("Question Text").GetComponent<Text>();

		_actorImage = GameObject.Find("Actor Image").GetComponent<Image>();

		_familyImage = GameObject.Find("Family Image").GetComponent<Image>();
		_friendImage = GameObject.Find("Friend Image").GetComponent<Image>();
		_coupleImage = GameObject.Find("Couple Image").GetComponent<Image>();
		_classImage = GameObject.Find("Class Image").GetComponent<Image>();
		
		ResetUi();
		ResetListeners();
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
		
		otherButton.onClick.RemoveAllListeners();
		otherButton.onClick.AddListener(delegate { CancelOutcome(); });
		
		_cancelAnswerButton.gameObject.SetActive(true);
		
		_familyImage.gameObject.SetActive(true);
		_friendImage.gameObject.SetActive(true);
		_coupleImage.gameObject.SetActive(true);
		_classImage.gameObject.SetActive(true);
		// TODO: add predictions from model
		var familyScale = Random.value * 4;
		_familyImage.gameObject.transform.localScale = new Vector3(familyScale, familyScale, familyScale);
		var friendScale = Random.value * 4;
		_friendImage.gameObject.transform.localScale = new Vector3(friendScale, friendScale, friendScale);
		var coupleScale = Random.value * 4;
		_familyImage.gameObject.transform.localScale = new Vector3(coupleScale, coupleScale, coupleScale);
		var classScale = Random.value * 4;
		_classImage.gameObject.transform.localScale = new Vector3(classScale, classScale, classScale);


	}

	private void CancelOutcome()
	{
		ResetUi();
		ResetListeners();
	}

	private void ApplyOutcome(Button outcomeButton)
	{
		ResetUi();
		ResetListeners();
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
