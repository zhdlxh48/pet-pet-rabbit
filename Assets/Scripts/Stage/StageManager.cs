using System;
using System.Collections;
using UnityEngine;

namespace Rabbit
{
    public enum StageState
    {
        ShowInfo, CountDown, Start, End
    }
    
    public class StageManager : MonoBehaviour
    {
        private static int _currentLevel;
        private static int _scorePresetIndex;

        public static int CurrentLevel => _currentLevel;
        public static int MaxLevelIndex => GameDataStorage.Instance.maxLevel - 1;
        
        public static StageState GameState { get; private set; }
        
        public static LevelData LevelData => GameDataStorage.Instance.GetLevelData(_currentLevel);
        public static ScoreData ScorePreset => GameDataStorage.Instance.GetScoreData(_scorePresetIndex);
        public static HealthData HealthPreset => GameDataStorage.Instance.GetHealthData(_scorePresetIndex);
        public static TimeData TimePreset => GameDataStorage.Instance.GetTimeData(_scorePresetIndex);

        private void Awake()
        {
            GameState = StageState.ShowInfo;
        }

        private void Start()
        {
            InitializeGameStatus();
        }

        private void InitializeGameStatus()
        {
            var tempStatus = FindObjectsOfType<GameStatus>();
            foreach (var status in tempStatus) {
                status.Initialize();
            }
        }

        public static void SetStageLevel(int level)
        {
            _currentLevel = level;
        }
        
        public static void SetStageState(string state)
        {
            GameState = (StageState) Enum.Parse(typeof(StageState), state);
        }
        public static void SetStageState(StageState state)
        {
            GameState = state;
        }
    }
}