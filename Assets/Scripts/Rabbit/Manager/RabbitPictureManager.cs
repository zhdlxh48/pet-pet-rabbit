using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Rabbit
{
    public enum RabbitPictureState
    {
        Sleeping, Dreaming, Nightmare
    }
    
    public class RabbitPictureManager : MonoBehaviour
    {
        [SerializeField] private Animator rabbitAnimator;
        
        [SerializeField] private EmojiEffect emojiEffector;
        [SerializeField] private RabbitRubHand rubHand;
        
        [SerializeField] private AnimationNameDictionary<RabbitPictureState> animationNameDic;
        public RabbitPictureState CurrentState { get; private set; }

        private GameCombo comboSystem;

        private void Awake()
        {
            comboSystem = FindObjectOfType<GameCombo>();
            
            CurrentState = RabbitPictureState.Sleeping;
        }

        private void Start()
        {
            rabbitAnimator.Play(animationNameDic[CurrentState]);
            emojiEffector.SetEmoji(CurrentState);
        }

        public void ComboOnGoingAnimation()
        {
            SetRabbitState(comboSystem.Combo >= 15 ? RabbitPictureState.Dreaming : RabbitPictureState.Sleeping);
        }

        public void ComboBreakAnimation()
        {
            SetRabbitState(RabbitPictureState.Nightmare);
        }

        private void SetRabbitState(RabbitPictureState state)
        {
            if (state == CurrentState) return;
            
            rabbitAnimator.Play(animationNameDic[CurrentState = state]);
            emojiEffector.SetEmoji(state);
            rubHand.SetHandState(state);
        }
    }
}