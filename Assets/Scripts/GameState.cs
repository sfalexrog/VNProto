using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneDayProto.Model
{

    public class GameState : MonoBehaviour
    {
        public int currentPower;
        public int maxPower;

        public string PlayerName;
        public PlayerGender PlayerGender = PlayerGender.Boy;

        void Awake()
        {
            currentPower = 2;
            maxPower = 3;

            PlayerName = "Игрок";
        }
    }

    public enum PlayerGender
    {
        Boy = 0,
        Girl = 1
    }
}