using UnityEngine;

namespace Rabbit
{
    public class Timer : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private float targetTime;
        [ReadOnly] [SerializeField] private float runningTime;

        public float RemainTime => targetTime - runningTime;
        public float RunningTime => runningTime;

        public bool IsTimerWorking { get; private set; }

        private void Awake()
        {
            targetTime = 0.0f;
            runningTime = 0.0f;

            IsTimerWorking = false;
        }
        
        private void Update()
        {
            if (!IsTimerWorking) return;
            
            if (targetTime > runningTime) {
                runningTime += Time.deltaTime;
            }
            else {
                runningTime = targetTime;
            }
        }
        
        public float SetTargetTime(float time)
        {
            return targetTime = time;
        }
        
        public float SetRunningTime(float time)
        {
            return runningTime = time;
        }

        public void Run()
        {
            IsTimerWorking = true;
        }

        public void Stop()
        {
            IsTimerWorking = false;
        }
    }
}