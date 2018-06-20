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
		
		ResetUi();
		ResetListeners();
	}

	private void ResetUi()
	{
		_leftAnswerButton.image.color = Color.white;
		_rightAnswerButton.image.color = Color.white;
		_cancelAnswerButton.gameObject.SetActive(false);
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
