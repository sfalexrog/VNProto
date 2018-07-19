using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubjectNerd.Utilities;
using OneDayProto.Model;

namespace OneDayProto.Card
{
    [CreateAssetMenu(fileName = "Card", menuName = "OneDay/Card/Card", order = 1)]
    public class Card : ScriptableObject 
	{
        public CardActorBase actor;
        [TextArea]
        public string question;

        [Header("Left Choice")]
        public string leftButton;
        [Reorderable]
        public Outcome[] leftOutcomes;
        
        [Header("Right Choice")]
        public string rightButton;
        [Reorderable]
        public Outcome[] rightOutcomes;

        [Space(10)]
        public BackgroundType background;

        [Header("For girl")]
        public Card girlVersionCard;

        //functions
        public Card GetInfoByGender(PlayerGender gender)
        {
            if (gender == PlayerGender.Girl && girlVersionCard != null)
            {
                return girlVersionCard;
            }
            return this;
        }
	}

    public enum BackgroundType
    {
        Blue = 0
    }
}
