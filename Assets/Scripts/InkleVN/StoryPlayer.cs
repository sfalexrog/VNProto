﻿using Ink.Runtime;
using InkleVN;
using OneDayProto.Novel;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OneDayProto.Model
{

    public class StoryPlayer : MonoBehaviour
    {
        private GameState _gameState;
        private NovelChapter _novelChapter;

        public class StoryChoice
        {
            public int ChoiceID; // Choice ID, as presented by Inkle
            public string ChoiceText;
            public string ChoiceType; // Good, neutral, or bad

            public StoryChoice(int choiceId, string choiceText, string choiceType)
            {
                ChoiceID = choiceId;
                ChoiceText = choiceText;
                ChoiceType = choiceType;
            }
        }

        public Button ProceedButton;

        public StoryUIController UI;
        private Story _story;

        // Valid actor names; actor names from scripts will be compared against this set
        private HashSet<string> _registeredActors;

        // Reserved name for player actor
        private static string _playerActorName = "Я";
        // Gender-specific player actor names
        private static string _playerBoyName = "Player Boy";
        private static string _playerGirlName = "Player Girl";

        // Current defender cost
        private int _currentDefenderCost = 1;

        void Start()
        {
            _gameState = Toolbox.RegisterComponent<GameState>();
            _novelChapter = _gameState.currentNovelChapter;

            _registeredActors = UI.GetActorNames();
            ProceedButton.onClick.AddListener(delegate { OnProceed(); });

            _story = new Story(_novelChapter.inkJsonAsset.text);
            _story.BindExternalFunction("isBoy", () => IsBoy());

            UI.SetOnChoiceHandler(OnChoice);
            UI.SetOnDefenderHandler(OnDefender);
            UI.SetCanUseDefenderHandler(CanUseDefender);
            UI.SetMaxDefenderCharge(_gameState.maxPower);
            UI.SetCurrentDefenderCharge(_gameState.currentPower);

            OnProceed();
        }

        private bool IsBoy()
        {
            return _gameState.PlayerGender == PlayerGender.Boy;
        }

        private class Phrase
        {
            public string ActorName;
            public string ActorEmotion;
            public string Text;
        }

        private Phrase ParsePhrase(string storyPhrase, List<string> tags)
        {
            Phrase parsedPhrase = new Phrase();
            var splitPhrase = storyPhrase.Split(':');
            // Check if there's an actor that says anything
            if (splitPhrase.Length > 1)
            {
                // Check against player actor name
                if (splitPhrase[0].Equals(_playerActorName))
                {
                    parsedPhrase.ActorName = IsBoy() ? _playerBoyName : _playerGirlName;
                    StringBuilder sb = new StringBuilder();

                    for (int i = 1; i < splitPhrase.Length; ++i)
                    {
                        sb.Append(splitPhrase[i]);
                        if (i < splitPhrase.Length - 1)
                        {
                            sb.Append(": ");
                        }
                    }

                    parsedPhrase.Text = sb.ToString();
                }
                else if (_registeredActors.Contains(splitPhrase[0]))
                {
                    parsedPhrase.ActorName = splitPhrase[0];
                    StringBuilder sb = new StringBuilder();

                    for (int i = 1; i < splitPhrase.Length; ++i)
                    {
                        sb.Append(splitPhrase[i]);
                        if (i < splitPhrase.Length - 1)
                        {
                            sb.Append(": ");
                        }
                    }

                    parsedPhrase.Text = sb.ToString();
                }
                else
                {
                    // Not an actor and not a player character -> the whole phrase
                    // is just a non-character phrase
                    parsedPhrase.Text = storyPhrase;
                }
            }
            else
            {
                // The whole phrase is text, there's no actor that speaks it
                parsedPhrase.Text = storyPhrase;
            }
            // A hack: if a tag only contains one word, it's an emotion
            foreach (var tag in tags)
            {
                var tagSplit = tag.Split(':');
                if (tagSplit.Length == 1)
                {
                    parsedPhrase.ActorEmotion = tag;
                    break;
                }
            }

            if (parsedPhrase.ActorName != null &&
                parsedPhrase.ActorEmotion == null)
            {
                parsedPhrase.ActorEmotion = "default";
            }

            return parsedPhrase;
        }

        private void OnChoice(int choice)
        {
            if (!UI.WillAcceptTransitions)
                return;
            _story.ChooseChoiceIndex(choice);
            OnProceed();
        }

        private bool CanUseDefender()
        {
            return _gameState.currentPower >= _currentDefenderCost;
        }

        private void OnDefender()
        {
            _gameState.currentPower -= _currentDefenderCost;
            UI.SetCurrentDefenderCharge(_gameState.currentPower);
        }

        private string RebuildString(string[] splitString, int offset)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = offset; i < splitString.Length; ++i)
            {
                sb.Append(splitString[i]);
                if (i < splitString.Length - 1)
                {
                    sb.Append(":");
                }
            }
            return sb.ToString();
        }

        /**
         * Create a scene transition, based on the current state
         */
        private SceneTransitionRequest CreateSceneTransition(SceneTransitionRequest prevRequest = null)
        {
            var transitionBuilder = new SceneTransitionRequest.Builder();
            if (prevRequest != null)
            {
                // Copy fileds from the previous transition
                // FIXME: right now we actually only care about background changes...
                transitionBuilder.SetBackground(prevRequest.TransitionBackground);
            }
            var storyText = _story.currentText;
            var storyTags = _story.currentTags;
            // Parse tags first, they will come in handy later
            var actorEmotion = "default";

            foreach (var tag in storyTags)
            {
                string[] tagComponents = tag.Split(' ');

                if (tagComponents.Length > 1)
                {
                    if (tagComponents[0].Equals("Location"))
                    {
                        // Location change. We should probably read the next line and re-run our function.
                        transitionBuilder.SetBackground(tagComponents[1].Trim());
                        _story.Continue();
                        return CreateSceneTransition(transitionBuilder.Build());
                    }
                }
                else
                {
                    // Keyless tags are interpreted as emotions
                    actorEmotion = tag;
                }
            }

            // Parse story text, possibly handling edge cases like location changes
            var splitText = storyText.Split(':');
            if (splitText.Length > 1)
            {
                if (splitText[0].Equals(_playerActorName))
                {
                    var speaker = IsBoy() ? _playerBoyName : _playerGirlName;
                    transitionBuilder.SetSpeaker(speaker, actorEmotion);
                    transitionBuilder.SetPhrase(RebuildString(splitText, 1).Trim());
                }
                else if (_registeredActors.Contains(splitText[0]))
                {
                    var speaker = splitText[0];
                    transitionBuilder.SetSpeaker(speaker, actorEmotion);
                    transitionBuilder.SetPhrase(RebuildString(splitText, 1).Trim());
                }
                else
                {
                    // No one is actually speaking, the colon is just part of the text
                    transitionBuilder.SetPhrase(storyText.Trim());
                }
            }
            else
            {
                // Set text only
                transitionBuilder.SetPhrase(storyText.Trim());
            }

            // Parse choices (if there are any)
            foreach (var choice in _story.currentChoices)
            {
                var splitChoice = choice.text.Split('@');
                var choiceType = "neutral";
                if (splitChoice.Length > 1)
                {
                    choiceType = splitChoice[1];
                }
                var storyChoice = new StoryChoice(choice.index, splitChoice[0], choiceType);
                transitionBuilder.AddChoice(storyChoice);
            }
            transitionBuilder.SetCanUseDefender(true);

            return transitionBuilder.Build();
        }

        public void OnProceed()
        {
            if (!UI.WillAcceptTransitions)
                return;
            if (_story.canContinue)
            {
                var nextStoryText = _story.Continue();
                var transition = CreateSceneTransition();
                UI.Transition(transition);
            }
            else
            {
                _gameState.NextLevel();
                SceneManager.LoadScene("Scenes/HubScene");
            }
        }
    }
}