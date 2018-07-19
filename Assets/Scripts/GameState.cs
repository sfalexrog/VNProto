using OneDayProto.Card;
using OneDayProto.Novel;
using UnityEngine;

namespace OneDayProto.Model
{

    public class GameState : MonoBehaviour
    {
        public int currentPower;
        public int maxPower;

        public string PlayerName;
        public PlayerGender PlayerGender = PlayerGender.Boy;

        public int currentLevelIndex { get; private set; }
        public NovelChapter currentNovelChapter { get; private set; }
        public CardMissionBase currentCardMission { get; private set; }

        //private
        private LevelSequence _levelSequence;

        void Awake()
        {
            currentPower = 2;
            maxPower = 3;

            PlayerName = "Игрок";
        }

        public void Initialize(LevelSequence levelSequence) {
            _levelSequence = levelSequence;
            UpdateLevels();
        }

        private void UpdateLevels()
        {
            currentNovelChapter = null;
            currentCardMission = null;

            LevelModelBase level = GetCurrentLevel();

            if (level is NovelChapter)
            {
                currentNovelChapter = level as NovelChapter;
            }
            else if (level is CardMissionBase)
            {
                currentCardMission = level as CardMissionBase;
            }
        }

        private LevelModelBase GetCurrentLevel()
        {
            if (_levelSequence.IsUseDebug())
            {
                if (_levelSequence.debugLevelIndex != -1)
                {
                    return _levelSequence.levels[_levelSequence.debugLevelIndex];
                }
                else if (_levelSequence.debugLevel != null)
                {
                    return _levelSequence.debugLevel;
                }
            }

            return _levelSequence.levels[currentLevelIndex];
        }

        public void NextLevel()
        {
            currentLevelIndex++;
            currentLevelIndex = Mathf.Min(currentLevelIndex, _levelSequence.levels.Length - 1);
            UpdateLevels();
        }
    }

    public enum PlayerGender
    {
        Boy = 0,
        Girl = 1
    }
}