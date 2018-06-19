using System;

public class PlayerPhraseEvent : DialogueEvent
{
    public override DialogueEventType Type
    {
        get { return DialogueEventType.TYPE_PLAYER_PHRASE; }
    }

    public int nextEventId;

    // Gender-neutral phrase
    public String phrase;

    // Gender-based phrase
    public String boyPhrase;
    public String girlPhrase;
    
}