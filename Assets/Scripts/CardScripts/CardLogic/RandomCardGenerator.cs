using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

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
            else
            {
                Debug.LogError("Card " + card.Id + " has no background defined!");
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
            else
            {
                Debug.LogError("Card " + card.Id + " has no actor defined!");
            }
        }

        return actors.ToArray();
    }
}
