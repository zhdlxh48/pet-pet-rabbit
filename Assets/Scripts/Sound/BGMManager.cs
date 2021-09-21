using System;
using FMOD;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using Debug = UnityEngine.Debug;

namespace Rabbit
{
    public class BGMManager : SoundManager<int>
    {
        [SerializeField] private int bgmNumber;
        [SerializeField] private bool IsPlayOnStart;
        
        private EventInstance bgmEventState;

        private void Awake()
        {
            bgmEventState = RuntimeManager.CreateInstance(eventDic[bgmNumber].soundEvent);
        }

        private void Start()
        {
            if (IsPlayOnStart) {
                BGMPlay();
            }
        }

        private void OnEnable()
        {
            BGMPause(true);
        }

        private void OnDisable()
        {
            BGMPause(false);
        }

        private void OnDestroy()
        {
            BGMStop();
        }

        public void BGMPlay()
        {
            var resultState = bgmEventState.start();
            if (RESULT.OK != resultState) {
                Debug.LogError($"[ERROR] FMOD ERROR : {resultState}");
            }
        }
        public void BGMPlay(int index)
        {
            BGMStop();
            bgmEventState = RuntimeManager.CreateInstance(eventDic[index].soundEvent);
            var resultState = bgmEventState.start();
            if (RESULT.OK != resultState) {
                Debug.LogError($"[ERROR] FMOD ERROR : {resultState}");
            }
        }

        public void BGMPause(bool play)
        {
            var resultState = bgmEventState.setPaused(!play);
            if (RESULT.OK != resultState) {
                Debug.LogError($"[ERROR] FMOD ERROR : {resultState}");
            }
        }

        public void BGMStop()
        {
            var resultState = bgmEventState.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            if (RESULT.OK != resultState) {
                Debug.LogError($"[ERROR] FMOD ERROR : {resultState}");
            }
        }
    }
}