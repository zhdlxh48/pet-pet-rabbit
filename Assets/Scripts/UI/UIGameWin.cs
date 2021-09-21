using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Rabbit
{
    public class UIGameWin : MonoBehaviour
    {
        [Header("다음 레벨 버튼 오브젝트")]
        [SerializeField] private GameObject NextLevelButton;

        [Header("Win 정보 표현에 필요한 Status")]
        [SerializeField] private GameRank rankSystem;
        [SerializeField] private GameScore scoreSystem;
        [SerializeField] private GameHealth healthSystem;
        [SerializeField] private GameCombo comboSystem;

        [Header("Rank 표현 텍스트")]
        [SerializeField] private TMP_Text rankText;
        [SerializeField] private TMP_Text rankDesc;
        
        [Header("Score 관련 표현 텍스트")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text comboText;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text failedText;

        [Header("Press Any Button Event")] 
        [SerializeField] private UnityEvent pressAnyKeyEvent;
        
        private void OnEnable()
        {
            //NextLevelAvailable();
            NextLevelButton.SetActive(true);
            PerfectComboCheck();
            
            DisplayStageScore();
        }

        private void Update()
        {
            PressKeyEvent();
        }

        /// <summary>
        /// 다음 레벨이 존재하는지 파악하고, 존재한다면 다음레벨로 가는 버튼을 Enable
        /// </summary>
        private void NextLevelAvailable()
        {
            if (StageManager.CurrentLevel == StageManager.MaxLevelIndex) {
                NextLevelButton.SetActive(false);
            }
            else {
                NextLevelButton.SetActive(true);
            }
        }

        /// <summary>
        /// 우승 시 퍼펙트 콤보인지 확인하고, 조건에 부합하면 이를 표시
        /// </summary>
        private void PerfectComboCheck()
        {
            if (StageManager.LevelData.count == comboSystem.Combo) {
                // TODO: 퍼펙트 콤보 시 할 것
            }
        }

        /// <summary>
        /// 스테이지의 스코어를 계산해서 Display
        /// </summary>
        private void DisplayStageScore()
        {
            var (rank, desc) = rankSystem.GetRank(scoreSystem.Score);
            
            rankText.text = rank;
            rankDesc.text = desc;
            scoreText.text = $"{scoreSystem.Score}";
            comboText.text = $"{comboSystem.HighestCombo}";
            healthText.text = $"{healthSystem.Health}";
            failedText.text = $"{comboSystem.Failed}";
        }

        private void PressKeyEvent()
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame) {
                if (StageManager.CurrentLevel == StageManager.MaxLevelIndex) {
                    SceneLoader.LoadScene("CreditScene");
                }
                else {
                    pressAnyKeyEvent.Invoke();
                }
            }
        }
    }
}