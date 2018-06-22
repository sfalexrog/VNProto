public class ScriptedCardGenerator : ICardGenerator
{
    private CardHeap _cards;
    private int[] _cardSequence;
    private int _currentIdx;

    public ScriptedCardGenerator(string cardScriptName)
    {
        // TODO: Load card script   
    }
    
    public Card YieldCard()
    {
        Card retCard = _cards.GetCardById(_cardSequence[_currentIdx]);
        _currentIdx += 1;
        return retCard;
    }

    public int GetNumCardsRemaining()
    {
        return _cardSequence.Length - _currentIdx;
    }

}
