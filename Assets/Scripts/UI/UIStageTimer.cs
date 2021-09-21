using System;
using TMPro;
using UnityEngine;

namespace Rabbit
{
    public class UIStageTimer : MonoBehaviour
    {
        [SerializeField] private GameTime gameTime;

        [SerializeField] private TMP_Text timerText;

        private void Awake()
        {
            this.enabled = false;
        }

        private void Update()
        {
            timerText.text = TimeTextFormat(Mathf.CeilToInt(gameTime.timer.RemainTime));
        }

        private static string TimeTextFormat(float time)
        {
            return $"{Mathf.Floor(time / 60):00} : {time % 60:00}";
        }
    }
}