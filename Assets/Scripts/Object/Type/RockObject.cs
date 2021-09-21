using System;
using System.Collections;
using UnityEngine;

namespace Rabbit
{
    public class RockObject : SubObject
    {
        [Range(0.0f, 1.0f)]
        [SerializeField] private float pushDistance;
        [SerializeField] private float animationSpeed;

        private bool isAnimationPlaying;
        
        private void Reset()
        {
            objectType = SubObjectType.Rock;
        }

        private void Awake()
        {
            InitializeHeight();

            isAnimationPlaying = false;
        }

        public override void PlayAnimation(int order)
        {
            if (order == 0) {
                isAnimationPlaying = false;
            }
        }

        public override void PlayAnimation(bool success)
        {
            if (!success) {
                StartCoroutine(RockHitAnimation());
            }
        }

        private IEnumerator RockHitAnimation()
        {
            if (isAnimationPlaying) yield break;
            isAnimationPlaying = true;

            var targetPos = new Vector3(transform.position.x - pushDistance, transform.position.y, transform.position.z);
            while (Vector3.Distance(transform.position, targetPos) > 0.02f) {
                transform.position =
                    Vector3.Lerp(transform.position, targetPos, Time.deltaTime * animationSpeed);
                yield return new WaitForEndOfFrame();
            }
            transform.position = targetPos;
            
            yield return new WaitWhile(() => isAnimationPlaying);
            
            transform.position = transform.parent.position + new Vector3(0.0f, originHeight, 0.0f);
        }
    }
}