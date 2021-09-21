using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Rabbit
{
    [CreateAssetMenu(fileName = "TimeData", menuName = "Stage/Create TimeData", order = 0)]
    public class TimeData : ScriptableObject
    {
        [System.Serializable]
        public class TimeScore
        {
            public float plus;
            public float minus;
        }
        
        [System.Serializable]
        public class NoteTimeDictionary : SerializableDictionaryBase<NoteType, TimeScore> { }

        public NoteTimeDictionary healthDic;
    }
}