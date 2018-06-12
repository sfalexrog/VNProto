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

    public String text;
    public String speakerName;
    public int nextEventId;

}
