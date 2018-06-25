using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

public class CardScript
{
    // Generator: "random" or "scripted"
    public string Generator;
    // Array of used card packs
    public string[] CardPacks;
    // IDs of cards used for boy character; ignored for "random" generator
    public int[] BoyCardIds;
    // IDs of cards used for girl character; ignored for "random" generator
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
            generator = new RandomCardGenerator(heap, cardScript.NumCards);
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
