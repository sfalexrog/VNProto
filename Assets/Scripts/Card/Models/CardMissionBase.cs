using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubjectNerd.Utilities;

namespace OneDayProto.Card
{
    public abstract class CardMissionBase : ScriptableObject 
	{
        [Reorderable]
        public CardPack[] cardPacks;
        public int needFinishCount;
        
        protected abstract void OnInitialize();
        protected abstract Card OnGetNext();
        protected abstract bool IsFinished();
        
        public void Initialize()
        {
            OnInitialize();
        }
    }
}
