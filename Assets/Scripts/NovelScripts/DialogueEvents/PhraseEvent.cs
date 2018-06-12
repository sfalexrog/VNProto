using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PhraseEvent : DialogueEvent
{
    public PhraseEvent()
    {
        this.Type = DialogueEventType.TYPE_PHRASE;
    }   
}
