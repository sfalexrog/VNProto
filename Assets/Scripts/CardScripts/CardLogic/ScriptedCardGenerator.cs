using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
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
    
    public override string[] GetCardBackgroundsNames()
    {
        var backgrounds = new HashSet<string>();
        foreach (var cardId in _cardSequence)
        {
            var card = _cards.GetCardById(cardId);
            if (card.Background != null)
            {
                backgrounds.Add(card.Background);    
            }
        }

        return backgrounds.ToArray();
    }

    public override string[] GetCardActorsNames()
    {
        var actors = new HashSet<string>();
        foreach (var cardId in _cardSequence)
        {
            var card = _cards.GetCardById(cardId);
            if (card.Actor != null)
            {
                actors.Add(card.Actor);    
            }
        }

        return actors.ToArray();
    }
}
