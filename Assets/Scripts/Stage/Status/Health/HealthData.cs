using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Rabbit
{
    [CreateAssetMenu(fileName = "HealthData", menuName = "Stage/Create HealthData", order = 0)]
    public class HealthData : ScriptableObject
    {
        [System.Serializable]
        public class HealthScore
        {
            public float heal;
            public float damage;
        }
        
        [System.Serializable]
        public class NoteHealthDictionary : SerializableDictionaryBase<NoteType, HealthScore> { }

        public NoteHealthDictionary healthDic;
    }
}