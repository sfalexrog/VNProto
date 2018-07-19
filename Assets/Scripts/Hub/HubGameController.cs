using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using OneDayProto.Model;
using OneDayProto.UI;

namespace OneDayProto.Controllers
{
    public class HubGameController : MonoBehaviour
    {
        public HubUiGlue UiGlue;
        public bool RestoreDefenderOnRestart;

        private GameState _gameState;


        void Awake()
        {
            _gameState = Toolbox.RegisterComponent<GameState>();
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
                //UiGlue.SetStoryModeText(_chapters[_gameState.currentScene].ChapterName);
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
            return false;
        }

        private bool CurrentLevelIsCard()
        {
            return false;
        }
    }
}