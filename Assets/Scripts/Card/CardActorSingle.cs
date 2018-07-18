using System.Collections;
using System.Collections.Generic;
using UnityEngine;

﻿namespace OneDayProto.Card
{
    [CreateAssetMenu(fileName = "Actor", menuName = "OneDay/Card/Actor", order = 2)]
    public class CardActorSingle : CardActorBase
    {
        public new string name;
        public Sprite Image;

        public override CardActorBase GetActor(PlayerGender gender)
        {
            return this;
        }
    }
}
