using UnityEngine;

namespace Rabbit
{
    public class StageSceneHandler : MonoBehaviour
    {
        public void StageLevelLoad(int level)
        {
            StageLoader.LoadStage(level);
        }

        public void ReloadLevelMove()
        {
            StageLoader.LoadStage(StageManager.CurrentLevel);
        }
        
        public void NextLevelMove()
        {
            StageLoader.LoadStage(StageManager.CurrentLevel + 1);
        }

        public void IntroMove()
        {
            SceneLoader.LoadSceneAsync("IntroScene");
            DestroyImmediate(GameDataStorage.Instance.gameObject);
        }

        public void IntroMoveImmidiately()
        {
            SceneLoader.LoadScene("IntroScene");
            DestroyImmediate(GameDataStorage.Instance.gameObject);
        }

        public void CreditScene()
        {
            SceneLoader.LoadScene("CreditScene");
        }
    }
}
