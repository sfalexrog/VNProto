using OneDayProto.Model;
using SubjectNerd.Utilities;

namespace OneDayProto.Card
{
    public abstract class CardMissionBase : LevelModelBase 
	{
        [Reorderable]
        public CardPack[] cardPacks;

        public Card currentCard { get; private set; }
        
        protected abstract void OnInitialize();
        protected abstract Card OnGetNext();
        public abstract bool IsFinished();
        public abstract int GetRestCount();
        
        public void Initialize()
        {
            OnInitialize();
        }

        public Card GetNext()
        {
            currentCard = OnGetNext();
            return currentCard;
        }
    }
}
