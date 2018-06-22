using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public class Outcome
    {
        public string Faction;
        public float Change;
    }
    
    public int Id;

    public string Question;
    public string Actor;
    
    public string LeftButtonText;
    public string RightButtonText;

    public Outcome[] LeftOutcomes;
    public Outcome[] RightOutcomes;
}
