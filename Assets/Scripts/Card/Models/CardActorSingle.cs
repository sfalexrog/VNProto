using OneDayProto.Model;
using UnityEngine;

namespace OneDayProto.Card
{
    [CreateAssetMenu(fileName = "Actor", menuName = "OneDay/Card/Actor", order = 2)]
    public class CardActorSingle : CardActorBase
    {
        public override CardActorBase GetActor(PlayerGender gender)
        {
            return this;
        }
    }
}
