namespace Rabbit
{
    public static class StageLoader
    {
        private const string StageSceneName = "GameScene";

        public static void LoadStage(int level)
        {
            StageManager.SetStageLevel(level);
            SceneLoader.LoadScene(StageSceneName);
        }

        public static void LoadStageAsync(int level)
        {
            StageManager.SetStageLevel(level);
            SceneLoader.LoadSceneAsync(StageSceneName);
        }
    }
}