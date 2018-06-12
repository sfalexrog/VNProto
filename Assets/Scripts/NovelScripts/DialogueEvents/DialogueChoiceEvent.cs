using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DialogueChoiceEvent : DialogueEvent
{
    public override DialogueEventType Type
    {
        get { return DialogueEventType.TYPE_CHOICE; }
    }

    public bool canUseDefender;
    public int defenderCost;

    public class ChoiceData
    {
        public String choiceText;
        public String defenderText;
        public int resultId;
    }

    public ChoiceData[] choices;

}
