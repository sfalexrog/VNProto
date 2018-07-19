using UnityEngine;
using UnityEngine.SceneManagement;
using OneDayProto.Model;

namespace OneDayProto.Card
{
    public class CardController : MonoBehaviour
    {
        private GameState _gameState;
        public CardMissionBase mission;

        // Relation values
        private float[] _relations;
        private const int MAX_RELATIONS = 10;

        void Awake()
        {
            _gameState = Toolbox.RegisterComponent<GameState>();
            mission = _gameState.currentCardMission;
            mission.Initialize();

            _relations = new float[4];
            for (int i = 0; i < 4; ++i)
            {
                _relations[i] = MAX_RELATIONS / 2;
            }
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

            Outcome[] outcomes;
            if (answer == 0) // left answer
            {
                outcomes = mission.currentCard.leftOutcomes;
            }
            else
            {
                outcomes = mission.currentCard.rightOutcomes;
            }

            foreach (var outcome in outcomes)
            {
                var idx = (int) outcome.faction;
                result[idx] = outcome.change;
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
        public bool IsGameOver()
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
            _gameState.NextLevel();
            SceneManager.LoadScene("Scenes/HubScene");
        }
    }
}