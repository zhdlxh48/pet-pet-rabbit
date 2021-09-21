using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rabbit
{
    public class IntroSceneHandler : MonoBehaviour
    {
        public void StartStage()
        {
            StageLoader.LoadStageAsync(0);
        }
        
        public void GameExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
