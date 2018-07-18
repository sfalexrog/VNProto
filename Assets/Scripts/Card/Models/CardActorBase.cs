using System.Collections;
using System.Collections.Generic;
using UnityEngine;

﻿namespace OneDayProto.Card
{
    public abstract class CardActorBase : ScriptableObject 
	{
        public new string name;
        public Sprite sprite;

        public abstract CardActorBase GetActor(PlayerGender gender);
	}
}
