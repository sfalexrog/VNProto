using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OneDayProto.Card
{

    public class CardUiGlue : MonoBehaviour
    {
        public CardController cardController;

        public GameObject _purposePanel;
        public GameObject _answerPanel;
        public GameObject _parametersPanel;
        public GameObject _leftAnswer;
        public GameObject _rightAnswer;

        public Button _leftAnswerButton;
        public Button _rightAnswerButton;
        public Button _cancelAnswerButton;

        public Text _leftAnswerText;
        public Text _rightAnswerText;

        public Text _nameText;
        public Text _questionText;

        public Text _goalText;

        public Image _actorImage;

        public Image _familyImage;
        public Image _friendImage;
        public Image _coupleImage;
        public Image _classImage;

        public Slider _familySlider;
        public Slider _friendSlider;
        public Slider _coupleSlider;
        public Slider _classSlider;

        private string _leftButtonStoredText;
        private string _rightButtonStoredText;

        private float[] _leftResults;
        private float[] _rightResults;

        private CardSliderAnimator[] _animations;

        // Popup "window" and background
        public GameObject _popupPanel;
        public Text _popupHeaderText;
        public Text _popupDescriptionText;
        public Button _dismissPopupButton;

        // Background image
        public Image _backgroundImage;

        // Bind to scene objects
        void Awake()
        {

            _cancelAnswerButton.onClick.AddListener(delegate { CancelOutcome(); });

            _leftAnswerText.text = "Left answer";
            _rightAnswerText.text = "Right answer";

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
            if (cardController.IsGameOverState())
            {
                DisplayGameOverState();
            }
            else
            {
                int cardsRemaining = cardController.mission.GetRestCount();
                _goalText.text = cardsRemaining.ToString();


                if (cardController.mission.IsFinished())
                {
                    DisplayWinState();
                }
                else
                {
                    Card card = cardController.mission.GetNext();

                    _leftResults = cardController.GetRelationsChange(0);
                    _rightResults = cardController.GetRelationsChange(1);
                    DisplayRelations();
                    DisplayCard(card);
                }
            }
        }

        private void DisplayCurrentCard()
        {
            DisplayCard(cardController.mission.currentCard);
        }

        private void DisplayCard(Card c)
        {
            Card card = c.GetInfoByGender(PlayerGender.Boy);

            _leftAnswerText.text = card.leftButton;
            _rightAnswerText.text = card.rightButton;

            _questionText.text = card.question;
            _actorImage.sprite = card.actor.sprite;
            //_backgroundImage.sprite = _backgroundSprites[cardController.GetBackgroundForCard(card)];
            _nameText.text = card.actor.name;

            // Store button text, just in case
            _leftButtonStoredText = card.leftButton;
            _rightButtonStoredText = card.rightButton;
        }

        private void DisplayRelations()
        {
            // TODO: convert to animations
            _animations[0] = new CardSliderAnimator(_familySlider, cardController.GetCurrentRelations(FactionType.Family), 1.0f);
            _animations[1] = new CardSliderAnimator(_friendSlider, cardController.GetCurrentRelations(FactionType.Friends), 1.0f);
            _animations[2] = new CardSliderAnimator(_coupleSlider, cardController.GetCurrentRelations(FactionType.Couple), 1.0f);
            _animations[3] = new CardSliderAnimator(_classSlider, cardController.GetCurrentRelations(FactionType.Class), 1.0f);
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

            _popupPanel.SetActive(false);
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
            otherButton.onClick.AddListener(delegate {
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

            var familyScale = Mathf.Abs(relationChange[(int) FactionType.Family]);
            _familyImage.gameObject.transform.localScale = new Vector3(familyScale, familyScale, familyScale);
            var friendScale = Mathf.Abs(relationChange[(int) FactionType.Friends]);
            _friendImage.gameObject.transform.localScale = new Vector3(friendScale, friendScale, friendScale);
            var coupleScale = Mathf.Abs(relationChange[(int) FactionType.Couple]);
            _coupleImage.gameObject.transform.localScale = new Vector3(coupleScale, coupleScale, coupleScale);
            var classScale = Mathf.Abs(relationChange[(int) FactionType.Class]);
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
                cardController.ApplyChange(0);
            }
            else if (outcomeButton == _rightAnswerButton)
            {
                cardController.ApplyChange(1);
            }
            ResetUi();
            Advance();
            ResetListeners();
        }


        // Update is called once per frame
        // Play any live animations
        void Update()
        {
            for (int i = 0; i < _animations.Length; ++i)
            {
                if (_animations[i] != null)
                {
                    _animations[i].Update();
                    if (_animations[i].IsEnded())
                    {
                        // Delete animation if it's ended
                        _animations[i] = null;
                    }
                }
            }
        }

        void DisplayGameOverState()
        {
            _popupPanel.SetActive(true);
            _popupHeaderText.text = "Вы проиграли";
            _popupDescriptionText.text = "Главное в жизни - баланс. Следите за отношениями со всеми!";

        }

        void DisplayWinState()
        {
            _popupPanel.SetActive(true);
            _popupHeaderText.text = "Вы победили!";
            _popupDescriptionText.text = "Ты сумел сохранить отношения со всеми. Миссия пройдена!";
        }

        public void OnPopupDismiss()
        {
            cardController.OnCardGameFinish();
        }
    }
}