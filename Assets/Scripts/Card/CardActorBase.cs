using System.Collections;
using System.Collections.Generic;
using UnityEngine;

﻿namespace OneDayProto.Card
{
    public abstract class CardActorBase : ScriptableObject 
	{
        public abstract CardActorBase GetActor(PlayerGender gender);
	}
}
