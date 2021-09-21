using System.Collections.Generic;
using UnityEngine;

namespace Rabbit
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Stage/Create LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public float health;
        public int count;
        public List<NoteType> types;
        public List<NoteType> infoShowTypes;
        public float levelTime;
    }
}