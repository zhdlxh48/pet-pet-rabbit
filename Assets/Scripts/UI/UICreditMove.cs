using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UICreditMove : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private Image fadeOutImage;
    
    [SerializeField] private float startHeight;
    [SerializeField] private float endHeight;
    
    [SerializeField] private float normalSpeed;
    [SerializeField] private float fastSpeed;

    [SerializeField] private UnityEvent creditEndEvent;

    [SerializeField] private float endWaitTime;
    [SerializeField] private float fadeOutInterval;
    
    private bool isCreditEnded = false;

    private float MoveSpeed => Keyboard.current.anyKey.isPressed ? fastSpeed : normalSpeed;

    private void Awake()
    {
        rectTransform.anchoredPosition = new Vector2(0.0f, startHeight);
    }

    private void Update()
    {
        if (rectTransform.anchoredPosition.y <= endHeight) {
            MoveUp(MoveSpeed);
        }
        else if (!isCreditEnded) {
            isCreditEnded = true;
            StartCoroutine(EndCredit());
        }
    }

    private void MoveUp(float speed)
    {
        transform.position += Vector3.up * (speed * Time.deltaTime);
    }

    private IEnumerator EndCredit()
    {
        yield return new WaitForSeconds(endWaitTime);
        
        while (fadeOutImage.color.a < 0.95f) {
            fadeOutImage.color = Color.Lerp(fadeOutImage.color, Color.black, 0.1f);
            yield return new WaitForSeconds(fadeOutInterval);
        }
        
        fadeOutImage.color = Color.black;
        creditEndEvent.Invoke();
    }
}
