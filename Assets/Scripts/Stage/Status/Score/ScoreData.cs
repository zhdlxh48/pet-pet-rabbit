using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Rabbit
{
    [CreateAssetMenu(fileName = "ScoreData", menuName = "Stage/Create ScoreData", order = 0)]
    public class ScoreData : ScriptableObject
    {
        [System.Serializable]
        public class NoteScoreDictionary : SerializableDictionaryBase<NoteType, float> { }

        public float defaultNoteScore;
        public NoteScoreDictionary scoreDic;
    }
}