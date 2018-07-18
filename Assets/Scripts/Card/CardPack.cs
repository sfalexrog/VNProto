using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubjectNerd.Utilities;

namespace OneDayProto.Card
{
    [CreateAssetMenu(fileName = "CardPack", menuName = "OneDay/Card/Pack", order = 1)]
    public class CardPack : ScriptableObject 
	{
        [Reorderable]
        public Card[] cards;
	}
}
