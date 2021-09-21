using System;
using System.Collections;
using Rabbit;
using UnityEngine;

public class RabbitRubHand : MonoBehaviour
{
    private enum HandState
    {
        Rubbing, Tapping
    }
    
    [SerializeField] private Animator handAnimator;
    [SerializeField] private Transform handTransform;
    
    [SerializeField] private AnimationNameDictionary<HandState> rubStateDictionary;

    [ReadOnly] [SerializeField] private HandState handState;
    
    [ReadOnly] [SerializeField] private Vector3 startHandPos;
    [SerializeField] private Vector3 moveLeftUpVector;
    [ReadOnly] [SerializeField] private Vector3 moveVector;
    
    [SerializeField] private float handSpeed;
    [ReadOnly] [SerializeField] private bool isStopHand;
    [ReadOnly] [SerializeField] private bool isLeftUp;

    private Coroutine moveRoutine;

    private void Awake()
    {
        handState = HandState.Rubbing;
        
        isStopHand = false;
        isLeftUp = true;
    }

    private void Start()
    {
        startHandPos = handTransform.position;

        handAnimator.Play(rubStateDictionary[HandState.Rubbing]);
        moveRoutine = StartCoroutine(CoroutineMove());
    }

    private void Update()
    {
        if (isStopHand) return;

        handTransform.position += 
            (isLeftUp ? moveLeftUpVector : -moveLeftUpVector) * (Time.deltaTime * handSpeed);
    }

    public void SetHandState(RabbitPictureState state)
    {
        if (state == RabbitPictureState.Sleeping || state == RabbitPictureState.Dreaming) {
            PlayAnimation(HandState.Rubbing);
        }
        else {
            PlayAnimation(HandState.Tapping);
        }
    }

    private void PlayAnimation(HandState state)
    {
        if (handState == state) return;
        
        handAnimator.Play(rubStateDictionary[handState = state]);
        handTransform.position = startHandPos;
        
        if (state == HandState.Rubbing) {
            isLeftUp = true;
            moveRoutine = StartCoroutine(CoroutineMove());
        }
        else {
            isStopHand = true;
            StopCoroutine(moveRoutine);
        }
    }
    

    private IEnumerator CoroutineMove()
    {
        while(true)
        {
            isStopHand = false;
            yield return new WaitForSeconds(3.0f);
            
            isStopHand = true;
            yield return new WaitForSeconds(1.0f);  
            
            isLeftUp = !isLeftUp;
        }
    }
}
