using OneDayProto.Model;
using UnityEngine;

namespace OneDayProto.Actor
{
    [CreateAssetMenu(fileName = "ActorGender", menuName = "OneDay/Actor/Gender", order = 2)]
    public class ActorGender : ActorModelBase 
	{
        public ActorSingle forBoyActor;
        public ActorSingle forGirlActor;

        public override string GetName(PlayerGender gender)
        {
            return GetActor(gender).GetName(gender);
        }

        public override Sprite GetSprite(PlayerGender gender)
        {
            return GetActor(gender).GetSprite(gender);
        }

        private ActorModelBase GetActor(PlayerGender gender)
        {
            if (gender == PlayerGender.Boy)
            {
                return forBoyActor;
            }
            return forGirlActor;
        }
    }
}
