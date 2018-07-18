using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

public class CardScript
{
    // Generator: "random" or "scripted"
    public string Generator;
    // Array of used card packs
    public string[] CardPacks;
    // IDs of cards used for boy character;
    public int[] BoyCardIds;
    // IDs of cards used for girl character;
    public int[] GirlCardIds;
    // Number of generated cards; ignored for "scripted" generator
    public int NumCards;
    // Reward for chapter completion
    public int CompletionReward;
}

public abstract class ICardGenerator
{
    public abstract Card YieldCard();
    public abstract int GetNumCardsRemaining();
    public abstract string[] GetCardBackgroundsNames();
    public abstract string[] GetCardActorsNames();

    private int _completionReward;

    public int CompletionReward
    {
        get { return _completionReward; }
    }

    public static ICardGenerator FromScript(string resourceName, int gender = 0)
    {
        var scriptJson = Resources.Load<TextAsset>(resourceName);
        var cardScript = JsonConvert.DeserializeObject<CardScript>(scriptJson.text);

        ICardGenerator generator = null;

        var heap = new CardHeap();
        
        foreach (var pack in cardScript.CardPacks)
        {
            heap.LoadCards(pack);
        }
        
        if (cardScript.Generator == "random")
        {
            int[] cardIds = null;
            if (gender == (int) PlayerGender.Boy)
            {
                cardIds = cardScript.BoyCardIds;
            }
            else
            {
                cardIds = cardScript.GirlCardIds;
            }
            generator = new RandomCardGenerator(heap, cardScript.NumCards, cardIds);
        }
        else if (cardScript.Generator == "scripted")
        {
            int[] cardSequence = null;
            if (gender == (int) PlayerGender.Boy)
            {
                cardSequence = cardScript.BoyCardIds;
            }
            else
            {
                cardSequence = cardScript.GirlCardIds;
            }
            generator = new ScriptedCardGenerator(heap, cardSequence);
        }
        else
        {
            Debug.LogError("Only 'random' and 'scripted' generators are supported!");
            return null;
        }

        generator._completionReward = cardScript.CompletionReward;

        return generator;
    }
    
}
