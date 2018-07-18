using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

﻿namespace OneDayProto.Card
{
    [Serializable]
	public class Outcome 
	{
        public FactionType faction;
        public float change;
	}

    public enum FactionType
    {
        Family = 0,
        Friends = 1,
        Couple = 2,
        Class = 3
    }
}
