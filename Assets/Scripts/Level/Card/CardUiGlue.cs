using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OneDayProto.Model;

namespace OneDayProto.Card
{
    public class CardUiGlue : MonoBehaviour
    {
        public CardController cardController;

        public GameObject _purposePanel;
        public GameObject _parametersPanel;


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

        public SwipableCard SwipeCard;
        private CanvasGroup _swipeCG;
        private CanvasGroup _popupCG;
        public Text AnswerText;
        public CanvasGroup AnswerTextShadow;

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
        
        // Interaction blocker - should be active during transitions
        public Image InteractionBlocker;

        [Header("Animation controls")]
        public AnimationCurve FadeOutCurve;
        public float FadeOutDuration;
        public AnimationCurve FadeInCurve;
        public float FadeInDuration;

        private enum Side
        {
            Left,
            Right
        };

        void Awake()
        {
            _swipeCG = SwipeCard.GetComponent<CanvasGroup>();
            _popupCG = _popupPanel.GetComponent<CanvasGroup>();
            SwipeCard.onCenter.AddListener(delegate { CancelOutcome(); });
            SwipeCard.onShiftLeft.AddListener(delegate { PreviewOutcome(Side.Left); });
            SwipeCard.onShiftRight.AddListener(delegate { PreviewOutcome(Side.Right); });
            SwipeCard.onSwipeLeft.AddListener(delegate { ApplyOutcome(Side.Left); });
            SwipeCard.onSwipeRight.AddListener(delegate { ApplyOutcome(Side.Right); });

            _animations = new CardSliderAnimator[4];
            _leftButtonStoredText = "";
            _rightButtonStoredText = "";
            InteractionBlocker.gameObject.SetActive(false);
            // FIXME: Store original colors during animations
            var fillBar = _familySlider.fillRect.GetComponentInChildren<Image>();
            CardSliderAnimator.OriginalColor = fillBar.color;
        }

        // Load a card and display it
        private void Start()
        {
            ResetUi();
            Advance();
        }

        private void Advance()
        {
            if (cardController.IsFailRelations())
            {
                StartCoroutine(ShowFinalScreen(false));
            }
            else
            {
                int cardsRemaining = cardController.mission.GetRestCount();
                _goalText.text = cardsRemaining.ToString();


                if (cardController.mission.IsFinished())
                {
                    StartCoroutine(ShowFinalScreen(true));
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

        // Animation coroutines
        IEnumerator FadeCG(CanvasGroup cg, float duration, float start, float end, AnimationCurve curve)
        {
            var timeStart = Time.time;
            var timeEnd = timeStart + duration;
            while (Time.time < timeEnd)
            {
                var t = (Time.time - timeStart) / (timeEnd - timeStart);
                var c = curve.Evaluate(t);
                cg.alpha = start * (1.0f - c) + end * c;
                yield return null;
            }
            cg.alpha = end;
            yield return null;
        }
        
        IEnumerator ShowNextCard()
        {
            SwipeCard.IgnoreInput = true;
            InteractionBlocker.gameObject.SetActive(true);
            yield return StartCoroutine(FadeCG(_swipeCG, FadeOutDuration, 1.0f, 0.0f, FadeOutCurve));
            SwipeCard.Reset();
            AnswerText.text = "";
            Advance();
            yield return StartCoroutine(FadeCG(_swipeCG, FadeInDuration, 0.0f, 1.0f, FadeInCurve));
            SwipeCard.IgnoreInput = false;
            InteractionBlocker.gameObject.SetActive(false);
        }

        IEnumerator ShowFinalScreen(bool win)
        {
            // Disable all input
            SwipeCard.IgnoreInput = true;
            InteractionBlocker.gameObject.SetActive(true);
            yield return StartCoroutine(FadeCG(_swipeCG, FadeOutDuration, 1.0f, 0.0f, FadeOutCurve));
            // Let changing animations play out
            // FIXME: Don't forget to unhardcode this value
            // when animations will be changed
            yield return new WaitForSeconds(0.8f - FadeOutDuration);
            if (win)
            {
                DisplayWinState();
            }
            else
            {
                DisplayGameOverState();
            }
            _popupCG.interactable = false;
            _popupCG.alpha = 0.0f;
            yield return StartCoroutine(FadeCG(_popupCG, FadeInDuration, 0.0f, 1.0f, FadeInCurve));
            _popupCG.interactable = true;
        }

        private void DisplayCurrentCard()
        {
            DisplayCard(cardController.mission.currentCard);
        }

        private void DisplayCard(Card c)
        {
            Card card = c.GetInfoByGender(PlayerGender.Boy);

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
            _familyImage.gameObject.SetActive(false);
            _friendImage.gameObject.SetActive(false);
            _coupleImage.gameObject.SetActive(false);
            _classImage.gameObject.SetActive(false);

            _popupPanel.SetActive(false);

            AnswerText.text = "";
        }


        private void PreviewOutcome(Side side)
        {

            _familyImage.gameObject.SetActive(true);
            _friendImage.gameObject.SetActive(true);
            _coupleImage.gameObject.SetActive(true);
            _classImage.gameObject.SetActive(true);
            float[] relationChange = null;
            if (side == Side.Left)
            {
                AnswerText.text = _leftButtonStoredText;
                relationChange = _leftResults;
            }
            else
            {
                AnswerText.text = _rightButtonStoredText;
                relationChange = _rightResults;
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
        }

        private void ApplyOutcome(Side side)
        {
            if (side == Side.Left)
            {
                cardController.ApplyChange(0);
            }
            else if (side == Side.Right)
            {
                cardController.ApplyChange(1);
            }
            // Set card as not interactable
            StartCoroutine(ShowNextCard());
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
            // Show answer text depending on the angle
            var angle = Mathf.Abs(SwipeCard.Angle);
            var minAngle = SwipeCard.ShiftAngle / 2.0f;
            var maxAngle = SwipeCard.ShiftAngle;
            var alpha = Mathf.Clamp01((angle - minAngle) / (maxAngle - minAngle));
            AnswerTextShadow.alpha = alpha;
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