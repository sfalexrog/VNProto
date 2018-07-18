using System;

public class PlayerPhraseEvent : DialogueEvent
{
    public override DialogueEventType Type
    {
        get { return DialogueEventType.TYPE_PLAYER_PHRASE; }
    }

    public int nextEventId;

    // Gender-neutral phrase
    public String text;

    // Gender-based phrase
    public String boyText;
    public String girlText;
    
    // Actor emotion
    public string emotion;
}