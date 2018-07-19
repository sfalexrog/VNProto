using SubjectNerd.Utilities;
using UnityEngine;

namespace OneDayProto.Model
{
    [CreateAssetMenu(fileName = "LevelSequence", menuName = "OneDay/LevelSequence")]
    public class LevelSequence : ScriptableObject 
	{
        [Header("Main")]
        [Reorderable]
        public LevelModelBase[] levels;

        [Header("Debug")]
        public int debugLevelIndex = -1;
        public LevelModelBase debugLevel;

        [Header("Gender")]
        public PlayerGender gender;

        public bool IsUseDebug()
        {
            if (Debug.isDebugBuild)
            {
                return debugLevelIndex != -1 || debugLevel != null;
            }
            return false;
        }
	}
}
