using UnityEngine;
using FMODUnity;
using RotaryHeart.Lib.SerializableDictionary;

namespace Rabbit
{
    [System.Serializable]
    public class SoundEvent
    {
        [EventRef] public string soundEvent;

        public SoundEvent()
        {
            soundEvent = "";
        }
    }

    public class SoundManager<T> : MonoBehaviour
    {
        [System.Serializable]
        public class SoundEventDictionary : SerializableDictionaryBase<T, SoundEvent> { }

        public SoundEventDictionary eventDic;
    }
}