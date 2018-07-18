using System.Collections;
using System.Collections.Generic;
using UnityEngine;

﻿namespace OneDayProto.Card
{
    [CreateAssetMenu(fileName = "ActorGender", menuName = "OneDay/Card/ActorGender", order = 2)]
    public class CardActorGender : CardActorBase 
	{
        public CardActorSingle forBoyActor;
        public CardActorSingle forGirlActor;

        public override CardActorBase GetActor(PlayerGender gender)
        {
            if (gender == PlayerGender.Boy)
            {
                return forBoyActor;
            }
            return forGirlActor;
        }
    }
}
