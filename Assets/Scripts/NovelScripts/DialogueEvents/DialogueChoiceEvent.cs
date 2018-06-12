using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DialogueChoiceEvent : DialogueEvent
{
    public DialogueChoiceEvent()
    {
        this.Type = DialogueEventType.TYPE_CHOICE;
    }
}
