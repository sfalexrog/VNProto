using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

public class PhraseEvent : DialogueEvent
{
    public override DialogueEventType Type
    {
        get { return DialogueEventType.TYPE_PHRASE; }
    }

    // Gender-neutral phrase
    public string text;
    
    // Gender-based phrase
    public string boyText;
    public string girlText;

    public string speakerName;
    
    // Gender-based actor
    public string boySpeakerName;
    public string girlSpeakerName;
    
    public int nextEventId;

    // Actor emotion
    public string emotion;
}
