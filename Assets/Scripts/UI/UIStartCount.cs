using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rabbit
{
    public class UIStartCount : MonoBehaviour
    {
        [SerializeField] private Image countImage;
        [SerializeField] private Sprite[] countTimeSprites;
        [SerializeField] private Sprite startSprite;

        [SerializeField] private float countIntervalTime;
        [SerializeField] private float startSpriteShowTime;

        [Space(10)] [SerializeField] private UnityEvent endCountEvent;
        
        private void Start()
        {
            StartCoroutine(CountDown());
        }

        private IEnumerator CountDown()
        {
            yield return new WaitUntil(() => StageManager.GameState == StageState.CountDown);
            
            foreach (var sprite in countTimeSprites) {
                countImage.sprite = sprite;
                yield return new WaitForSeconds(countIntervalTime);
            }

            countImage.sprite = startSprite;
            yield return new WaitForSeconds(startSpriteShowTime);
            countImage.enabled = false;
            
            endCountEvent.Invoke();
        }
    }
}