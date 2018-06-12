using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueEventType
{
    TYPE_PHRASE = 0,
    TYPE_CHOICE = 1,
    TYPE_END = 999
}

[Serializable]
public class DialogueEvent
{
    public DialogueEventType Type = DialogueEventType.TYPE_END;
    public int Id;
}
