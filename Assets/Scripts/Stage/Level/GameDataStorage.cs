using UnityEngine;

namespace Rabbit
{
    public class GameDataStorage : MonoSingleton<GameDataStorage>
    {
        public int maxLevel;
        
        [SerializeField] private LevelData[] levelData;
        [SerializeField] private ScoreData[] scoreData;
        [SerializeField] private HealthData[] healthData;
        [SerializeField] private TimeData[] timeData;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        public LevelData GetLevelData(int index)
        {
            return levelData[index];
        }

        public ScoreData GetScoreData(int index)
        {
            return scoreData[index];
        }

        public HealthData GetHealthData(int index)
        {
            return healthData[index];
        }

        public TimeData GetTimeData(int index)
        {
            return timeData[index];
        }
    }
}