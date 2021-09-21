using UnityEngine;

namespace Rabbit
{
    public class GameScore : GameStatus
    {
        [ReadOnly] public float Score;

        public override void Initialize()
        {
            Score = 0;
        }

        public void GetScore(NoteType type)
        {
            Score += StageManager.ScorePreset.scoreDic[type];
        }
        
        public void GetScore(NoteType type, int combo)
        {
            Score += StageManager.ScorePreset.scoreDic[type] * combo;
        }
    }
}