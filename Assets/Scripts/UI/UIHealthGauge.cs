using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rabbit
{
    public class UIHealthGauge : MonoBehaviour
    {
        [SerializeField] private GameHealth healthSystem;

        [SerializeField] private Image[] healthImages;

        private float healthRatio => healthSystem.Health / healthSystem.MaxHealth * healthImages.Length;

        public void GaugeUpdate()
        {
            if (StageManager.GameState != StageState.Start) return;

            var tempHealth = healthRatio;

            foreach (var image in healthImages) {
                image.fillAmount = tempHealth;
                tempHealth = Mathf.Clamp(tempHealth - 1.0f, 0.0f, healthRatio);
            }
        }
    }
}