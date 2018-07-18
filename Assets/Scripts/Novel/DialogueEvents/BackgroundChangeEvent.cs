using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChangeEvent : DialogueEvent
{
    public override DialogueEventType Type
    {
        get { return DialogueEventType.TYPE_BG_CHANGE; }
    }

    public string backgroundName;
    public string boyBackgroundName;
    public string girlBackgroundName;
    public int nextEventId;
}
