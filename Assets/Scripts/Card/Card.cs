using System.Collections;
using System.Collections.Generic;
using UnityEngine;

﻿namespace OneDayProto.Card
{
    [CreateAssetMenu(fileName = "Card", menuName = "OneDay/Card/Card", order = 1)]
    public class Card : ScriptableObject 
	{
        [TextArea]
        public string question;
        public CardCharacter character;

        [Header("Left Choice")]
        public string leftButton;
        public List<Outcome> leftOutcomes;
        
        [Header("Right Choice")]
        public string rightButton;
        public List<Outcome> rightOutcomes;

        public BackgroundType background;
	}

    public enum BackgroundType
    {
        Blue = 0
    }
}
