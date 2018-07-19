using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneDayProto.Model;

﻿namespace OneDayProto.Actor
{
    public abstract class ActorModelBase : ScriptableObject 
	{
        public abstract string GetName(PlayerGender gender);
        public abstract Sprite GetSprite(PlayerGender gender);
	}
}
