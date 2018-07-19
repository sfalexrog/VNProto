using OneDayProto.Model;
using UnityEngine;

namespace OneDayProto.Novel
{
    [CreateAssetMenu(fileName = "NovelChapter", menuName = "OneDay/Novel/Chapter", order = 1)]
    public class NovelChapter : LevelModelBase 
	{
        public new string name;
        [TextArea]
        public string description;
        public TextAsset inkJsonAsset;
	}
}
