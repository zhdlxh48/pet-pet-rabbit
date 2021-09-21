using UnityEngine;

namespace Rabbit
{
    public enum RabbitPlayState
    {
        Idle, FrontFootUp, RearFootUp, Jump, Crash, Bend
    }
    
    public class RabbitPlayManager : MonoBehaviour
    {
        [SerializeField] private RabbitSpriteChanger spriteChanger;
        [SerializeField] private SFXManager<RabbitPlayState> sfxManager;
        
        [ReadOnly] [SerializeField] private RabbitPlayState currentState;
        
        [SerializeField] private float rockJumpHigh;
        private Vector3 rabbitDefaultPos;

        private void Start()
        {
            rabbitDefaultPos = transform.position;
            SetState(RabbitPlayState.Idle);
        }

        public void RabbitWalk()
        {
            if (currentState == RabbitPlayState.FrontFootUp)
                SetState(RabbitPlayState.RearFootUp);
            else if (currentState == RabbitPlayState.RearFootUp)
                SetState(RabbitPlayState.FrontFootUp);
            else
                SetState(RabbitPlayState.FrontFootUp);
        }

        public void SetStateBySubObject(SubObjectType type, bool success = true)
        {
            switch (type) {
                case SubObjectType.Eagle:
                    SetState(success ? RabbitPlayState.Bend : RabbitPlayState.Crash);
                    break;
                case SubObjectType.Rock:
                    SetState(success ? RabbitPlayState.Jump : RabbitPlayState.Crash);
                    break;
                default:
                    Debug.LogError("[ERROR] PlayObjectType이 올바르지 않습니다. Idle로 전환됩니다.");
                    SetState(RabbitPlayState.Idle);
                    break;
            }
        }

        public void SetState(RabbitPlayState state)
        {
            if (state == currentState) return;
            
            sfxManager.PlayOneShot(state);
            spriteChanger.SetSprite(currentState = state);
            transform.position = (state == RabbitPlayState.Jump)
                ? rabbitDefaultPos + Vector3.up * rockJumpHigh
                : rabbitDefaultPos;
        }
    }
}