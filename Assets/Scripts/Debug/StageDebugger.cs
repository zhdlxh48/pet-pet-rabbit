using System;
using TMPro;
using UnityEngine;

namespace Rabbit
{
    public class StageDebugger : MonoBehaviour
    {
        [SerializeField] private GameCombo comboSystem;
        [SerializeField] private GameScore scoreSystem;
        [SerializeField] private GameHealth healthSystem;
        
        [SerializeField] private TMP_Text comboText;
        [SerializeField] private TMP_Text failedText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text healthText;
        
        private void Update()
        {
            comboText.text = $"Combo : {comboSystem.Combo}";
            failedText.text = $"Failed : {comboSystem.Failed}";
            scoreText.text = $"Score : {scoreSystem.Score}";
            healthText.text = $"Health : {healthSystem.Health}";
        }

        public void StageLevelChange(int level)
        {
            StageLoader.LoadStage(level);
        }
    }
}
