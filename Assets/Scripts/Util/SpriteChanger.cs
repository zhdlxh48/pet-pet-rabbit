using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Rabbit
{
    public class SpriteChanger<T> : MonoBehaviour where T : System.Enum
    {
        [System.Serializable]
        protected class SpriteDictionary : SerializableDictionaryBase<T, Sprite> { }

        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected SpriteDictionary spriteDic;

        public virtual void SetSprite(T state)
        {
            spriteRenderer.sprite = spriteDic[state];
        }
    }
}