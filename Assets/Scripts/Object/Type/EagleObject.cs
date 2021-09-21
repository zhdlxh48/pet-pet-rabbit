using System;
using System.Collections;
using UnityEngine;

namespace Rabbit
{
    public class EagleObject : SubObject
    {
        public enum EagleState
        {
            WingUp, WingDown, LandReady, PushReady, Push
        }

        [SerializeField] private EagleSpriteChanger eagleSpriteChanger;
        
        [Range(0.0f, 1.0f)]
        [SerializeField] private float pushDistance;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float landDistance;
        [SerializeField] private float animationSpeed;

        private bool isAnimationPlaying;

        [SerializeField] private float landReadyHeight;
        [SerializeField] private float pushReadyHeight;
        [SerializeField] private float eaglePushHeight;
        
        private void Reset()
        {
            objectType = SubObjectType.Eagle;
        }

        private void Awake()
        {
            InitializeHeight();

            isAnimationPlaying = false;
        }

        public void Move(Vector3 vec)
        {
            transform.position += vec;
        }

        public override void PlayAnimation(int order)
        {
            switch (order) {
                case 2:
                    SetHeight(landReadyHeight);
                    eagleSpriteChanger.SetSprite(EagleState.LandReady);
                    break;
                case 1:
                    SetHeight(pushReadyHeight);
                    eagleSpriteChanger.SetSprite(EagleState.PushReady);
                    break;
                case 0:
                    SetHeight(eaglePushHeight);
                    isAnimationPlaying = false;
                    eagleSpriteChanger.SetSprite(EagleState.Push);
                    break;
                default:
                    eagleSpriteChanger.SetSprite(order % 2 == 1 ? EagleState.WingUp : EagleState.WingDown);
                    break;
            }
        }


        public override void PlayAnimation(bool success)
        {
            if (!success) {
                StartCoroutine(EagleHitAnimation());
            }
        }

        private IEnumerator EagleHitAnimation()
        {
            if (isAnimationPlaying) yield break;
            isAnimationPlaying = true;

            var targetPos = new Vector3(transform.position.x - pushDistance, transform.position.y - landDistance, transform.position.z);
            while (Vector3.Distance(transform.position, targetPos) > 0.02f) {
                transform.position =
                    Vector3.Lerp(transform.position, targetPos, Time.deltaTime * animationSpeed);
                yield return new WaitForEndOfFrame();
            }
            transform.position = targetPos;
            
            yield return new WaitWhile(() => isAnimationPlaying);
            
            transform.position = transform.parent.position + new Vector3(0.0f, eaglePushHeight, 0.0f);
        }
    }
}