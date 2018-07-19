using OneDayProto.Model;
using UnityEngine;

namespace OneDayProto.Actor
{
    [CreateAssetMenu(fileName = "ActorSingle", menuName = "OneDay/Actor/Single", order = 1)]
    public class ActorSingle : ActorModelBase
    {

        public new string name;
        public Sprite sprite;

        public override string GetName(PlayerGender gender)
        {
            return name;
        }

        public override Sprite GetSprite(PlayerGender gender)
        {
            return sprite;
        }
    }
}
