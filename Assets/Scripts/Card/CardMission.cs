using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubjectNerd.Utilities;

namespace OneDayProto.Card
{
    [CreateAssetMenu(fileName = "CardMission", menuName = "OneDay/Card/Mission", order = 1)]
    public class CardMission : ScriptableObject 
	{
        [Reorderable]
        public CardPack[] cards;
    }
}
