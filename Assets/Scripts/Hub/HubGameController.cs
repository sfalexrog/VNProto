using OneDayProto.Model;
using OneDayProto.UI;
using UnityEngine;

namespace OneDayProto.Controllers
{
    public class HubGameController : MonoBehaviour
    {
        public HubUiGlue UiGlue;
        public LevelSequence levelSequence;
        public bool RestoreDefenderOnRestart;

        private GameState _gameState;
        
        void Awake()
        {
            _gameState = Toolbox.RegisterComponent<GameState>();
            _gameState.Initialize(levelSequence);
        }

        // Use this for initialization
        void Start()
        {
            if (RestoreDefenderOnRestart)
            {
                _gameState.currentPower = _gameState.maxPower;
            }
            UiGlue.DisableAllButtons();
            UiGlue.SetPowerMeterMaxValue(_gameState.maxPower);
            UiGlue.SetPowerMeterValue(_gameState.currentPower);
            
            if (CurrentLevelIsNovel())
            {
                UiGlue.SetStoryButtonEnabled();
                UiGlue.SetStoryModeText(_gameState.currentNovelChapter.name);
            }
            else
            {
                UiGlue.SetStoryButtonDisabled();
            }

            if (CurrentLevelIsCard())
            {
                UiGlue.SetCardButtonEnabled();
            }
            else
            {
                UiGlue.SetCardButtonDisabled();
            }
        }

        private bool CurrentLevelIsNovel()
        {
            return _gameState.currentNovelChapter != null;
        }

        private bool CurrentLevelIsCard()
        {
            return _gameState.currentCardMission != null;
        }
    }
}