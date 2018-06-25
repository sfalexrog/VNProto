using System;

public class RandomCardGenerator : ICardGenerator
{
    private CardHeap _cards;
    private int[] _cardSequence;
    private int _currentIdx;
    
    public RandomCardGenerator(CardHeap cards, int generatedCount)
    {
        _cards = cards;
        _cardSequence = new int[generatedCount];
        _currentIdx = 0;
        // TODO: factor in card frequencies
        var rng = new Random();
        var keys = _cards.GetCardIndices();
        for (int i = 0; i < _cardSequence.Length; ++i)
        {
            int keyIdx = rng.Next(keys.Length);
            _cardSequence[i] = keys[keyIdx];
        }
    }

    public override Card YieldCard()
    {
        Card retCard = _cards.GetCardById(_cardSequence[_currentIdx]);
        _currentIdx += 1;
        return retCard;
    }

    public override int GetNumCardsRemaining()
    {
        return _cardSequence.Length - _currentIdx;
    }
}
