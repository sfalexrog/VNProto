/**
 * A container for the cards. Allows reading specific cards into itself
 * and getting cards by their ID.
 */

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class CardHeap
{
    private Dictionary<int, Card> _cards;
    
    public CardHeap()
    {
        _cards = new Dictionary<int, Card>();
    }

    public void LoadCards(string resourceName)
    {
        var cardsText = Resources.Load<TextAsset>(resourceName);
        var cardsList = JsonConvert.DeserializeObject<List<Card>>(cardsText.text);
        
        foreach(var card in cardsList)
        {
            if (_cards.ContainsKey(card.Id))
            {
                Debug.LogWarning("Card id " + card.Id + " already present, replacing...");
            }

            _cards[card.Id] = card;
        }
    }

    public int[] GetCardIndices()
    {
        return _cards.Keys.ToArray();
    }

    public Card GetCardById(int cardIndex)
    {
        return _cards[cardIndex];
    }
    
}
