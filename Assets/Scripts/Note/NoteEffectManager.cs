using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Rabbit
{
    public class NoteEffectManager : MonoBehaviour
    {
        [System.Serializable]
        public class EffectTextureDictionary : SerializableDictionaryBase<NoteType, Texture2D> { }
        
        [SerializeField] private GameObject hitEffectObject;

        [SerializeField] private EffectTextureDictionary textureDic;

        private Queue<GameObject> hitEffectQueue;
        
        private void Start()
        {
            EffectPooling();
        }

        public void EffectShot(NoteType type)
        {
            var tempEffect = hitEffectQueue.Dequeue();
            tempEffect.SetActive(true);
            tempEffect.GetComponent<NoteHitEffect>().Effect(textureDic[type]);
        }

        private void EffectPooling()
        {
            hitEffectQueue = new Queue<GameObject>();
            for (var i = 0; i < StageManager.LevelData.count; i++) {
                var tempObj = Instantiate(hitEffectObject, transform);
                tempObj.SetActive(false);
                hitEffectQueue.Enqueue(tempObj);
            }
        }
    }
}