using System.Collections;
using System.Collections.Generic;
using UnityEngine;

﻿namespace OneDayProto.Card
{
    [CreateAssetMenu(fileName = "Characted", menuName = "OneDay/Card/Character", order = 2)]
    public class CardCharacter : ScriptableObject 
	{
        public new string name;
        public Sprite Image;
	}
}
