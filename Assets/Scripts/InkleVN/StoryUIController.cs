using System.Collections;
using System.Collections.Generic;
using GraphicsPools;
using InkleVN;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class StoryUIController : MonoBehaviour {

    private ActorPool _actorPool;
    private BackgroundPool _backgroundPool;

    [Header("UI elements")]
    public Image Background;
    public Image BackgroundTransition;
    public Image PhraseBackground;
    public Text PhraseText;
    public Image PlayerActorNameBox;
    public Text PlayerActorName;
    public Image PlayerActorImage;
    public Image NPCActorNameBox;
    public Text NPCActorName;
    public Image NPCActorImage;
    


    public Button TapTarget;

    [Header("UI prototype elements")]

    public Image UIContainer;
    public Button ChoiceButtonPrototype;
    public float ChoiceButtonDistance;

    [Header("Animation controls")]
    public float FadeOutDuration;
    public AnimationCurve FadeOutCurve;

    public float FadeInDuration;
    public AnimationCurve FadeInCurve;

    public float TransitionDuration;
    public AnimationCurve TransitionCurve;

    [Header("Animation targets")]
    public Image PlayerPhraseBackgroundAnchor;
    public Image PlayerPhraseTextAnchor;
    public Image DescriptionBackgroundAnchor;
    public Image DescriptionTextAnchor;
    public Image NPCPhraseBackgroundAnchor;
    public Image NPCPhraseTextAnchor;
    public Image ChoiceBackgroundAnchor;
    public Image ChoiceTextAnchor;

    [Header("Choice colors")]
    public Color GoodChoice;
    public Color NeutralChoice;
    public Color BadChoice;

    public delegate void OnChoice(int choiceIndex);

    private OnChoice _choiceHandler;

    private Text _shadowText;

    private Button[] _choiceButtons;

    // Actor image and name canvas group (used to show/hide both actor image and actor name box)
    private CanvasGroup _playerActorGroup;
    private CanvasGroup _NPCActorGroup;

    public void SetOnChoiceHandler(OnChoice handler)
    {
        _choiceHandler = handler;
    }

    private bool _willAcceptTransitions;

    public bool WillAcceptTransitions => _willAcceptTransitions;

    // Animation for RectTransform
    private class RectAnimation
    {
        public float TimeStart;
        public float TimeEnd;

        public AnimationCurve Curve;
        public RectTransform Target;

        public Vector2 InitialPosition;
        public Vector2 TargetPosition;

        public Vector2 InitialSize;
        public Vector2 TargetSize;

        public bool DidFinish;

        public RectAnimation(RectTransform target, float timeStart, float duration, AnimationCurve curve,
            RectTransform targetTransform)
        {
            Target = target;
            TimeStart = timeStart;
            TimeEnd = timeStart + duration;
            Curve = curve;
            InitialPosition = target.anchoredPosition;
            TargetPosition = targetTransform.anchoredPosition;
            InitialSize = new Vector2(target.rect.width, target.rect.height);
            TargetSize = new Vector2(targetTransform.rect.width, targetTransform.rect.height);
            DidFinish = TimeEnd < TimeStart;

        }
    }

    // Animation for Graphic-derivatives
    private class FadeAnimation
    {
        public float TimeStart;
        public float TimeEnd;

        public AnimationCurve Curve;
        public Graphic Target;

        // Opacity values for text (since we're only doing that)
        public float InitialAlpha;
        public float TargetAlpha;

        public bool DidFinish;

        // Convenience constructor
        public FadeAnimation(Graphic target, float timeStart, float duration, AnimationCurve curve,
            float targetAlpha)
        {
            Target = target;
            TimeStart = timeStart;
            TimeEnd = timeStart + duration;
            Curve = curve;
            //InitialAlpha = target.color.a;
            // hack: initial alpha is always 1-targetAlpha
            InitialAlpha = 1.0f - targetAlpha;
            TargetAlpha = targetAlpha;
            DidFinish = TimeEnd < TimeStart;
        }
    }

    // Pseudo-animation to set text
    private class SetTextAnimation
    {
        // We do this momentarilly, so no duration here.
        public float TimeSet;

        public Text Target;
        public string TargetText;

        public bool DidFinish;

        public SetTextAnimation(Text target, float timeSet, string targetText = null)
        {
            TimeSet = timeSet;
            Target = target;
            TargetText = targetText;
            DidFinish = false;
        }
    }

    // Pseudo-animation to set sprite
    private class SetSpriteAnimation
    {
        public float TimeSet;

        public Image Target;
        public Sprite TargetSprite;

        public bool DidFinish;

        public SetSpriteAnimation(Image target, float timeSet, Sprite targetSprite)
        {
            TimeSet = timeSet;
            Target = target;
            TargetSprite = targetSprite;
            DidFinish = false;
        }
    }

    // Animation to fade in/out a canvas group
    private class FadeCGAnimation
    {
        public float TimeStart;
        public float TimeEnd;

        public AnimationCurve Curve;
        public CanvasGroup Target;

        // Opacity values for text (since we're only doing that)
        public float InitialAlpha;
        public float TargetAlpha;

        public bool DidFinish;

        // Convenience constructor
        public FadeCGAnimation(CanvasGroup target, float timeStart, float duration, AnimationCurve curve,
            float targetAlpha)
        {
            Target = target;
            TimeStart = timeStart;
            TimeEnd = timeStart + duration;
            Curve = curve;
            //InitialAlpha = target.color.a;
            // hack: initial alpha is always 1-targetAlpha
            InitialAlpha = 1.0f - targetAlpha;
            TargetAlpha = targetAlpha;
            DidFinish = TimeEnd < TimeStart;
        }
    }



    // Animation group: animations that should be played simultaneously.
    // Only one animation group will be played at a time.
    private class AnimGroup
    {
        // Start and end times for animation group. They are populated automatically.
        public float TimeStart;
        public float TimeEnd;

        // Animation lists for the current group
        public List<RectAnimation> RectAnimations;
        public List<FadeAnimation> FadeAnimations;
        public List<SetTextAnimation> SetTextAnimations;
        public List<SetSpriteAnimation> SetSpriteAnimations;
        public List<FadeCGAnimation> FadeCGAnimations;

        public AnimGroup()
        {
            TimeEnd = TimeStart = Time.time;
            RectAnimations = new List<RectAnimation>();
            FadeAnimations = new List<FadeAnimation>();
            SetTextAnimations = new List<SetTextAnimation>();
            SetSpriteAnimations = new List<SetSpriteAnimation>();
            FadeCGAnimations = new List<FadeCGAnimation>();
        }

        public AnimGroup AddAnimation(RectAnimation ra)
        {
            TimeStart = Mathf.Min(TimeStart, ra.TimeStart);
            TimeEnd = Mathf.Max(TimeEnd, ra.TimeEnd);
            RectAnimations.Add(ra);
            return this;
        }

        public AnimGroup AddAnimation(FadeAnimation ta)
        {
            TimeStart = Mathf.Min(TimeStart, ta.TimeStart);
            TimeEnd = Mathf.Max(TimeEnd, ta.TimeEnd);
            FadeAnimations.Add(ta);
            return this;
        }

        public AnimGroup AddAnimation(SetTextAnimation sta)
        {
            TimeStart = Mathf.Min(TimeStart, sta.TimeSet);
            TimeEnd = Mathf.Max(TimeEnd, sta.TimeSet);
            SetTextAnimations.Add(sta);
            return this;
        }
        
        public AnimGroup AddAnimation(SetSpriteAnimation ssa)
        {
            TimeStart = Mathf.Min(TimeStart, ssa.TimeSet);
            TimeEnd = Mathf.Max(TimeEnd, ssa.TimeSet);
            SetSpriteAnimations.Add(ssa);
            return this;
        }

        public AnimGroup AddAnimation(FadeCGAnimation fcga)
        {
            TimeStart = Mathf.Min(TimeStart, fcga.TimeStart);
            TimeEnd = Mathf.Max(TimeEnd, fcga.TimeEnd);
            FadeCGAnimations.Add(fcga);
            return this;
        }
    }

    // Note: this used to be a queue, but with our use case, it's quite improbable
    // to have more than a handful of pending animations, and accessing the last
    // item is more important for us.
    private List<AnimGroup> _pendingAnimations;

    void Awake()
    {
        _willAcceptTransitions = true;
        _actorPool = new ActorPool(false);
        _backgroundPool = new BackgroundPool(false);
        _pendingAnimations = new List<AnimGroup>();
        _playerActorGroup = PlayerActorImage.GetComponent<CanvasGroup>();
        _NPCActorGroup = NPCActorImage.GetComponent<CanvasGroup>();

        // Hide all player groups by default
        _playerActorGroup.alpha = 0.0f;
        _NPCActorGroup.alpha = 0.0f;
    }

    /**
	 * Get registered actor names. This is required to distinguish between
	 * a "real" actor name and something that just happened to have a colon before it.
	 */
    public HashSet<string> GetActorNames()
    {
        return _actorPool.GetActorNames();
    }

    private float GetDesiredTextWidth(Text textComponent, string textContents, Vector2 requestedSize)
    {
        if (_shadowText == null)
        {
            _shadowText = Instantiate<Text>(textComponent);
            _shadowText.color = new Color(0, 0, 0, 0);
        }
        _shadowText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, requestedSize.x);
        _shadowText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, requestedSize.y);

        _shadowText.text = textContents;
        // Return a slightly larger value to fit the text
        return Mathf.Ceil(LayoutUtility.GetPreferredWidth(_shadowText.rectTransform));
    }

    private float GetDesiredTextHeight(Text textComponent, string textContents, Vector2 requestedSize)
    {
        if (_shadowText == null)
        {
            _shadowText = Instantiate<Text>(textComponent);
            _shadowText.color = new Color(0, 0, 0, 0);
        }
        _shadowText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, requestedSize.x);
        _shadowText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, requestedSize.y);

        _shadowText.text = textContents;
        _shadowText.rectTransform.ForceUpdateRectTransforms();
        // Return a slightly larger value to fit the text
        return Mathf.Ceil(LayoutUtility.GetPreferredHeight(_shadowText.rectTransform)) + 3.0f;
    }

    /**
     * Looks at the animation queue and returns the time of the last animation.
     * If there are no pending animations, returns current frame time.
     */
    private float GetNextAnimTime()
    {
        if (_pendingAnimations.Count > 0)
        {
            return _pendingAnimations[_pendingAnimations.Count - 1].TimeEnd;
        }
        return Time.time;
    }

    // Animate actor group hiding
    private void HideActor(CanvasGroup actorCGroup)
    {
        var timeStart = GetNextAnimTime();
        // Account for floats being floats
        if (actorCGroup.alpha < 0.001f) return;
        var animGroup = new AnimGroup();
        animGroup.AddAnimation(new FadeCGAnimation(actorCGroup, timeStart, FadeOutDuration, FadeOutCurve, 0.0f));
        _pendingAnimations.Add(animGroup);
    }

    // Animate all actor groups hiding
    private void HideAllActors()
    {
        HideActor(_NPCActorGroup);
        HideActor(_playerActorGroup);
    }
    /*
    private void ShowActor(SceneTransitionRequest str)
    {
        var timeOffset = 0.0f;
        // Fix up actor name for player
        var actorName = str.TransitionSpeaker;
        if (actorName.Contains("Player"))
        {
            actorName = "Я";
        }
        // If the actor name is already on screen, change it gracefully
        if (_actorGroup.alpha > 0.99f)
        {
            if (ActorName.text != actorName)
            {
                var animGroup = new AnimGroup();
                animGroup.AddAnimation(new FadeAnimation(ActorName, Time.time, FadeOutDuration, FadeOutCurve, 0.0f))
                    .AddAnimation(new SetTextAnimation(ActorName, Time.time + FadeOutDuration, actorName))
                    .AddAnimation(new FadeAnimation(ActorName, Time.time + FadeOutDuration, FadeInDuration, FadeInCurve, 1.0f));
                timeOffset = FadeOutDuration + FadeInDuration;
                _pendingAnimations.Enqueue(animGroup);
            }
        }
        else
        {
            // Change the name without any animation, the fade in from the canvas group will do the rest
            ActorName.text = actorName;
            _pendingAnimations.Enqueue(new AnimGroup().AddAnimation(new FadeCGAnimation(_actorGroup, Time.time + timeOffset, FadeInDuration, FadeInCurve, 1.0f)));
        }
    }
    */
    private void TransitionActor(SceneTransitionRequest str)
    {
        var timeStart = GetNextAnimTime();
        
        var timeOffset = 0.0f;
        var actorName = str.TransitionSpeaker;
        // Flag for player character - we do things a bit different for him
        var isPlayerCharacter = false;
        // Fix up actor name for player
        if (actorName.Contains("Player"))
        {
            actorName = "Я";
            isPlayerCharacter = true;
        }

        // Check if any actor group is displayed
        var isPCDisplayed = (_playerActorGroup.alpha > 0.99f);
        var isNPCDisplayed = (_NPCActorGroup.alpha > 0.99f);

        var animGroup = new AnimGroup();
        // Should we add group to the animation list?
        var addGroup = false;

        // Transition to player character
        if (isPlayerCharacter)
        {
            // Check if we need to hide NPC group
            if (isNPCDisplayed)
            {
                HideActor(_NPCActorGroup);
                // Update our animation time
                timeStart = GetNextAnimTime();
            }
            var pcSprite = _actorPool.GetActorSprite(str.TransitionSpeaker, str.TransitionSpeakerEmotion);
            var currentPcSprite = PlayerActorImage.sprite;

            if (isPCDisplayed)
            {
                // If player character is displayed, but has a different sprite (emotion),
                // fade it out and replace with an appropriate one
                if (currentPcSprite != pcSprite)
                {
                    animGroup.AddAnimation(new FadeAnimation(PlayerActorImage, timeStart, FadeOutDuration, FadeOutCurve, 0.0f));
                    animGroup.AddAnimation(new SetSpriteAnimation(PlayerActorImage, timeStart + FadeOutDuration, pcSprite));
                    animGroup.AddAnimation(new FadeAnimation(PlayerActorImage, timeStart + FadeOutDuration, FadeInDuration, FadeInCurve, 1.0f));
                    addGroup = true;
                }
                // There's not much to do otherwise
            }
            else
            {
                // We can actually swap images when the group is not shown
                if (currentPcSprite != pcSprite)
                {
                    animGroup.AddAnimation(new SetSpriteAnimation(PlayerActorImage, timeStart + FadeOutDuration, pcSprite));
                }
                animGroup.AddAnimation(new FadeCGAnimation(_playerActorGroup, timeStart, FadeInDuration, FadeInCurve, 1.0f));
                addGroup = true;
            }
        }
        else
        {
            // Check if we need to hide player character
            if (isPCDisplayed)
            {
                HideActor(_playerActorGroup);
                // Again, update animation time
                timeStart = GetNextAnimTime();
            }
            // Get the NPC sprite
            var npcSprite = _actorPool.GetActorSprite(str.TransitionSpeaker, str.TransitionSpeakerEmotion);
            var currentNpcSprite = NPCActorImage.sprite;
            var currentNpcName = NPCActorName.text;

            if (isNPCDisplayed)
            {
                // We might still need to change the NPC picture
                if (currentNpcSprite != npcSprite)
                {
                    animGroup.AddAnimation(new FadeAnimation(NPCActorImage, timeStart, FadeOutDuration, FadeOutCurve, 0.0f));
                    animGroup.AddAnimation(new SetSpriteAnimation(NPCActorImage, timeStart + FadeOutDuration, npcSprite));
                    animGroup.AddAnimation(new FadeAnimation(NPCActorImage, timeStart + FadeOutDuration, FadeInDuration, FadeInCurve, 1.0f));
                    addGroup = true;
                }
                // ...and possibly his/her name
                if (currentNpcName != str.TransitionSpeaker)
                {
                    animGroup.AddAnimation(new FadeAnimation(NPCActorName, timeStart, FadeOutDuration, FadeOutCurve, 0.0f));
                    animGroup.AddAnimation(new SetTextAnimation(NPCActorName, timeStart + FadeOutDuration, str.TransitionSpeaker));
                    animGroup.AddAnimation(new FadeAnimation(NPCActorName, timeStart + FadeOutDuration, FadeInDuration, FadeInCurve, 1.0f));
                    addGroup = true;
                }
            }
            else
            {
                // Animate group fade in and possibly change sprites/text while it's hidden
                if (currentNpcSprite != npcSprite)
                {
                    animGroup.AddAnimation(new SetSpriteAnimation(NPCActorImage, timeStart, npcSprite));
                }
                if (currentNpcName != str.TransitionSpeaker)
                {
                    animGroup.AddAnimation(new SetTextAnimation(NPCActorName, timeStart, str.TransitionSpeaker));
                }
                animGroup.AddAnimation(new FadeCGAnimation(_NPCActorGroup, timeStart, FadeInDuration, FadeInCurve, 1.0f));
                addGroup = true;
            }
        }
        if (addGroup)
        {
            _pendingAnimations.Add(animGroup);
        }
    }

    private void TransitionBackground(SceneTransitionRequest str)
    {
        var animGroup = new AnimGroup();
        BackgroundTransition.sprite = _backgroundPool.GetBackgroundSprite(str.TransitionBackground.ToLower());
        animGroup.AddAnimation(new FadeAnimation(BackgroundTransition, Time.time, FadeInDuration, FadeInCurve, 1.0f))
            .AddAnimation(new SetSpriteAnimation(Background, Time.time + FadeInDuration, BackgroundTransition.sprite))
            .AddAnimation(new FadeAnimation(BackgroundTransition, Time.time + FadeInDuration, 0.001f, FadeOutCurve, 0.0f));
        _pendingAnimations.Add(animGroup);
    }

    private void TransitionMainText(SceneTransitionRequest str, Image anchorTarget, Image anchorTextTarget)
    {
        // Calculate margins for text background
        var marginTopBottom = (anchorTarget.rectTransform.rect.height - anchorTextTarget.rectTransform.rect.height);

        var animGroup = new AnimGroup();
        var textFadeOut = new FadeAnimation(PhraseText, GetNextAnimTime(), FadeOutDuration, FadeOutCurve, 0.0f);
        animGroup.AddAnimation(textFadeOut);

        var lastAnimFinish = textFadeOut.TimeEnd;

        var defaultTextBoxSize = new Vector2(anchorTextTarget.rectTransform.rect.width,
                                            anchorTextTarget.rectTransform.rect.height);

        var requiredHeight = GetDesiredTextHeight(PhraseText, str.TransitionPhrase, defaultTextBoxSize);

        // Resize text and text background appropriately
        if (PhraseBackground.rectTransform.anchoredPosition != anchorTarget.rectTransform.anchoredPosition ||
            !Mathf.Approximately(requiredHeight, PhraseText.rectTransform.rect.height))
        {
            var textBgResize = new RectAnimation(PhraseBackground.rectTransform, textFadeOut.TimeEnd, TransitionDuration, TransitionCurve, anchorTarget.rectTransform);
            // Fixup for correct size
            textBgResize.TargetSize.y = requiredHeight + marginTopBottom;
            animGroup.AddAnimation(textBgResize);
            lastAnimFinish = textBgResize.TimeEnd;
            // Add short (less than 1 frame) animation for text
            var textBoxResize = new RectAnimation(PhraseText.rectTransform, textBgResize.TimeEnd, 0.001f, TransitionCurve, anchorTextTarget.rectTransform);
            textBoxResize.TargetSize.y = requiredHeight;
            animGroup.AddAnimation(textBoxResize);
        }

        var textChange = new SetTextAnimation(PhraseText, lastAnimFinish, str.TransitionPhrase);
        animGroup.AddAnimation(textChange);
        var textFadeIn = new FadeAnimation(PhraseText, lastAnimFinish, FadeInDuration, FadeInCurve, 1.0f);
        animGroup.AddAnimation(textFadeIn);
        _pendingAnimations.Add(animGroup);
    }

    private void CreateChoices(SceneTransitionRequest str)
    {
        var animGroup = new AnimGroup();
        var curTime = GetNextAnimTime();
        if (_choiceButtons != null)
        {
            foreach(var btn in _choiceButtons)
            {
                Destroy(btn);
            }
        }
        _choiceButtons = new Button[str.TransitionChoices.Length];
        for(int i = 0; i < str.TransitionChoices.Length; ++i)
        {
            var choiceButton = Instantiate<Button>(ChoiceButtonPrototype, UIContainer.transform);
            var choice = str.TransitionChoices[i];
            choiceButton.gameObject.SetActive(true);

            var choiceButtonCG = choiceButton.GetComponent<CanvasGroup>();
            choiceButtonCG.alpha = 0.0f;
            animGroup.AddAnimation(new FadeCGAnimation(choiceButtonCG, curTime + i * FadeInDuration, FadeInDuration, FadeInCurve, 1.0f));

            choiceButton.image.rectTransform.anchoredPosition = ChoiceButtonPrototype.image.rectTransform.anchoredPosition
                                                                + new Vector2(0.0f, -i * (ChoiceButtonPrototype.image.rectTransform.rect.height + ChoiceButtonDistance));

            choiceButton.onClick.AddListener(delegate { _choiceHandler(choice.ChoiceID); });

            var choiceButtonText = choiceButton.GetComponentInChildren<Text>();
            choiceButtonText.text = choice.ChoiceText;

            _choiceButtons[i] = choiceButton;
        }
        _pendingAnimations.Add(animGroup);
    }

    private void HideChoices()
    {
        if (_choiceButtons != null)
        {
            foreach(var btn in _choiceButtons)
            {
                btn.gameObject.SetActive(false);
                Destroy(btn);
            }
            _choiceButtons = null;
        }
    }
    
	
	/**
	 * Transition scene to another state
	 */
	public bool Transition(SceneTransitionRequest str)
	{
        HideChoices();
		if (!_willAcceptTransitions) return false;
        // Re-enable generic tap target if it was disabled before
        TapTarget.gameObject.SetActive(true);
        if (str.TransitionBackground != null)
        {
            TransitionBackground(str);
        }
        if (str.TransitionSpeaker == null)
		{
            HideAllActors();
            TransitionMainText(str, DescriptionBackgroundAnchor, DescriptionTextAnchor);
		}
		else
		{
            TransitionActor(str);
            
			//ActorImage.sprite = _actorPool.GetActorSprite(str.TransitionSpeaker, str.TransitionSpeakerEmotion);
			if (str.TransitionChoices == null)
			{
				if (str.TransitionSpeaker.Contains("Player"))
				{        
                    TransitionMainText(str, PlayerPhraseBackgroundAnchor, PlayerPhraseTextAnchor);
				}
				else
				{
                    TransitionMainText(str, NPCPhraseBackgroundAnchor, NPCPhraseTextAnchor);
				}
			}
            else
            {
                // Disable generic tap target
                TapTarget.gameObject.SetActive(false);
                TransitionMainText(str, ChoiceBackgroundAnchor, ChoiceTextAnchor);
                CreateChoices(str);
            }
		}

		return true;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_pendingAnimations.Count > 0)
		{
			// Disable tap target during animation
			_willAcceptTransitions = false;
			// Play back animations
			var animGroup = _pendingAnimations[0];
			var t = Time.time;
			foreach (var textAnim in animGroup.FadeAnimations)
			{
				if (t < textAnim.TimeStart || 
				    textAnim.DidFinish) continue;
				var dt = (t - textAnim.TimeStart) / (textAnim.TimeEnd - textAnim.TimeStart);
				var c = textAnim.Curve.Evaluate(dt);
				var currentColor = textAnim.Target.color;
				currentColor.a = c * textAnim.TargetAlpha + (1 - c) * textAnim.InitialAlpha;
				textAnim.Target.color = currentColor;
				if (t > textAnim.TimeEnd) textAnim.DidFinish = true;
			}

            foreach (var cgAnim in animGroup.FadeCGAnimations)
            {
                if (t < cgAnim.TimeStart ||
                    cgAnim.DidFinish) continue;
                var dt = (t - cgAnim.TimeStart) / (cgAnim.TimeEnd - cgAnim.TimeStart);
                var c = cgAnim.Curve.Evaluate(dt);
                cgAnim.Target.alpha = c * cgAnim.TargetAlpha + (1 - c) * cgAnim.InitialAlpha;
                if (t > cgAnim.TimeEnd) cgAnim.DidFinish = true;
            }

            foreach (var rectAnim in animGroup.RectAnimations)
			{
				if (t < rectAnim.TimeStart ||
				    rectAnim.DidFinish) continue;
				var dt = (t - rectAnim.TimeStart) / (rectAnim.TimeEnd - rectAnim.TimeStart);
				var c = rectAnim.Curve.Evaluate(dt);
				var transitionPosition = c * rectAnim.TargetPosition + (1 - c) * rectAnim.InitialPosition;
				var transitionSize = c * rectAnim.TargetSize + (1 - c) * rectAnim.InitialSize;
				rectAnim.Target.anchoredPosition = transitionPosition;
				rectAnim.Target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, transitionSize.x);
				rectAnim.Target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, transitionSize.y);
				if (t > rectAnim.TimeEnd) rectAnim.DidFinish = true;
			}

            foreach(var setText in animGroup.SetTextAnimations)
            {
                if (t >= setText.TimeSet && !setText.DidFinish)
                {
                    setText.Target.text = setText.TargetText;
                    setText.DidFinish = true;
                }
            }

            foreach(var setSprite in animGroup.SetSpriteAnimations)
            {
                if (t >= setSprite.TimeSet && !setSprite.DidFinish)
                {
                    setSprite.Target.sprite = setSprite.TargetSprite;
                    setSprite.DidFinish = true;
                }
            }

			if (t > animGroup.TimeEnd)
			{
				_pendingAnimations.RemoveAt(0);
			}
			
		}
		else
		{
			_willAcceptTransitions = true;
		}
	}
}
