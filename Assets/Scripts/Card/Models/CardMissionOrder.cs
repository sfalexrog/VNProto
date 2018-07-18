using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Collections.Generic;

﻿namespace OneDayProto.Card
{
    [CreateAssetMenu(fileName = "CardMission", menuName = "OneDay/Card/Mission", order = 2)]
    public class CardMissionOrder : CardMissionBase 
	{
        public bool isRandom = true;

        public int cardIndex { get; private set; }
        protected List<Card> allCards;

        protected override void OnInitialize()
        {
            cardIndex = -1;
            FillCardList();

            if (isRandom)
            {
                allCards.Shuffle();
            }
        }

        private void FillCardList()
        {
            allCards = new List<Card>();
            foreach (var pack in cardPacks)
            {
                foreach (var card in pack.cards)
                {
                    allCards.Add(card);
                }
            }
        }

        protected override Card OnGetNext()
        {
            cardIndex++;
            return allCards[cardIndex];
        }

        protected override bool IsFinished()
        {
            if (isRandom)
            {
                return (cardIndex >= needFinishCount - 1);
            }
            else
            {
                return (cardIndex >= allCards.Count - 1);
            }
        }
    }
}
