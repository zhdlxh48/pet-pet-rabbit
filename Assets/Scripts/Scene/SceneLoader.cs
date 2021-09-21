using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rabbit
{
    public class SceneLoader : MonoBehaviour
    {
        private static string _nextSceneName;
    
        private void Start()
        {
            StartCoroutine(LoadScene());
        }

        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public static void LoadSceneAsync(string sceneName)
        {
            _nextSceneName = sceneName;
            SceneManager.LoadScene("LoadingScene");
        }
        
        private static IEnumerator LoadScene()
        {
            yield return null;

            var op = SceneManager.LoadSceneAsync(_nextSceneName);
            op.allowSceneActivation = false;

            while (!op.isDone) {
                yield return null;
                if (op.progress >= 0.9f) {
                    yield return new WaitForSeconds(0.8f); //DEBUG
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}