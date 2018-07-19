using SubjectNerd.Utilities;
using UnityEngine;

namespace OneDayProto.Model
{
    [CreateAssetMenu(fileName = "LevelSequence", menuName = "OneDay/LevelSequence")]
    public class LevelSequence : ScriptableObject 
	{
        [Reorderable]
        public LevelModelBase[] levels;
	}
}
