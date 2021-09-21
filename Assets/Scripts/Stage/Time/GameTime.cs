using UnityEngine;

namespace Rabbit
{
    public class GameTime : GameStatus
    {
        public Timer timer;

        public bool IsTimeOver => timer.RemainTime <= 0.0f;

        public override void Initialize()
        {
            timer.SetTargetTime(StageManager.LevelData.levelTime);
        }

        public void PlayTimer()
        {
            timer.Run();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        public void ReduceTime(NoteType type)
        {
            timer.SetRunningTime(timer.RunningTime + StageManager.TimePreset.healthDic[type].minus);
        }
    }
}