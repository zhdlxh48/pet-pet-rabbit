using System.Diagnostics.CodeAnalysis;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Rabbit
{
    public class EmojiEffect : MonoBehaviour
    {
        [SerializeField] private ObjectDictionary<RabbitPictureState, GameObject> emojiDic;

        public void SetEmoji(RabbitPictureState state)
        {
            foreach (var item in emojiDic.Values) {
                item.SetActive(false);
            }
            emojiDic[state].SetActive(true);
        }
    }
}