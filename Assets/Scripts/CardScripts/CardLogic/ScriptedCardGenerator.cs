using System.CodeDom.Compiler;
using Newtonsoft.Json;
using UnityEngine;

public class ScriptedCardGenerator : ICardGenerator
{
    private CardHeap _cards;
    private int[] _cardSequence;
    private int _currentIdx;

    public ScriptedCardGenerator(CardHeap heap, int[] cardSequence)
    {
        _cards = heap;
        _cardSequence = cardSequence;
        _currentIdx = 0;
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
