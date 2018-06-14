using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDialogueEvent : DialogueEvent {
    public override DialogueEventType Type
    {
        get { return DialogueEventType.TYPE_END; }
    }

    public string text;
    public bool isChapterCompleted;
}
