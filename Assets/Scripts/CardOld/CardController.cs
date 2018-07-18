using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OneDayProto.Card
{
    public class CardController : MonoBehaviour
    {
        private GameState _gameState;

        private ICardGenerator _generator;

        // Relation values
        private float[] _relations;

        private const int MAX_RELATIONS = 10;

        // Actor name -> actor resource mapping
        private Dictionary<string, DialogueActor> _actors;
        // Background name -> background resource mapping
        private Dictionary<string, string> _backgrounds;

        void Awake()
        {
            _gameState = Toolbox.RegisterComponent<GameState>();

            _generator = ICardGenerator.FromScript(_gameState.CardScriptResource, (int) _gameState.PlayerGender);

            // TODO: Load relations from GameState

            _relations = new float[4];
            for (int i = 0; i < 4; ++i)
            {
                _relations[i] = MAX_RELATIONS / 2;
            }

            _actors = new Dictionary<string, DialogueActor>();

            // Load actor data
            var actorJson = Resources.Load<TextAsset>("Actors/manifest");
            var actors = JsonConvert.DeserializeObject<List<DialogueActor>>(actorJson.text);
            foreach (var actor in actors)
            {
                _actors[actor.name] = actor;
            }

            _backgrounds = new Dictionary<string, string>();
            // Load background data
            var backgroundsJson = Resources.Load<TextAsset>("Backgrounds/manifest");
            var backgrounds = JsonConvert.DeserializeObject<List<DialogueBackground>>(backgroundsJson.text);
            foreach (var background in backgrounds)
            {
                _backgrounds[background.name] = background.image;
            }

        }

        private CardOld _currentCard;

        public CardOld CurrentCard
        {
            get
            {
                return _currentCard;
            }
        }

        public CardOld GetNextCard()
        {
            _currentCard = _generator.YieldCard();
            return _currentCard;
        }

        public int GetCardsRemaining()
        {
            return _generator.GetNumCardsRemaining();
        }

        public float GetCurrentRelations(FactionType faction)
        {
            return _relations[(int) faction];
        }

        public float GetMaxRelations(FactionType faction)
        {
            return MAX_RELATIONS;
        }

        public float[] GetRelationsChange(int answer)
        {
            var result = new float[4];

            CardOld.Outcome[] outcomes;
            if (answer == 0) // left answer
            {
                outcomes = _currentCard.LeftOutcomes;
            }
            else
            {
                outcomes = _currentCard.RightOutcomes;
            }

            foreach (var outcome in outcomes)
            {
                var idx = -1;
                switch (outcome.Faction)
                {
                    case "Семья":
                        idx = (int) FactionType.Family;
                        break;
                    case "Класс":
                        idx = (int) FactionType.Class;
                        break;
                    case "Друзья":
                        idx = (int) FactionType.Friends;
                        break;
                    case "Пара":
                        idx = (int) FactionType.Couple;
                        break;
                    default:
                        Debug.LogWarning("Unknown faction: " + outcome.Faction);
                        break;
                }

                if (idx >= 0)
                {
                    result[idx] = outcome.Change;
                }
            }

            return result;
        }

        public void ApplyChange(int answer)
        {
            var change = GetRelationsChange(answer);
            for (int i = 0; i < 4; ++i)
            {
                _relations[i] += change[i];
            }
        }

        /**
         * Check if the game is over
         */
        public bool IsGameOverState()
        {
            bool result = false;
            foreach (var relation in _relations)
            {
                if ((relation <= 0) || (relation >= MAX_RELATIONS))
                {
                    result = true;
                }
            }

            return result;
        }

        /**
         * Finish the game
         */
        public void OnCardGameFinish()
        {
            // Disable progress by setting next card game ID to -1
            if (IsGameOverState())
            {
                _gameState.NextCardGameId = -1;
            }
            else
            {
                // Apply completion reward
                _gameState.currentExperience += _generator.CompletionReward;
            }
            SceneManager.LoadScene("Scenes/HubScene");
        }

        public string[] GetUsedBackgrounds()
        {
            var backgroundResources = new HashSet<string>();
            var backgroundNames = _generator.GetCardBackgroundsNames();
            foreach (var bgName in backgroundNames)
            {
                backgroundResources.Add(_backgrounds[bgName]);
            }

            return backgroundResources.ToArray();
        }

        public string[] GetUsedActors()
        {
            var actorResources = new HashSet<string>();
            var actorNames = _generator.GetCardActorsNames();
            foreach (var actorName in actorNames)
            {
                foreach (var res in _actors[actorName].emotions.Values)
                {
                    actorResources.Add(res);
                }
            }

            return actorResources.ToArray();
        }

        public string GetBackgroundForCard(CardOld card)
        {
            return _backgrounds[card.Background];
        }

        public string GetActorForCard(CardOld card)
        {
            // TODO: emotions?
            return _actors[card.Actor].emotions["default"];
        }

    }
}