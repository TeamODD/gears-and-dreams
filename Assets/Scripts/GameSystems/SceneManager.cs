using UnityEngine;
using UnityEngine.SceneManagement;

namespace GearsAndDreams.GameSystems
{
    public class CustomSceneManager : Singleton<CustomSceneManager>
    {
        public void LoadScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        public void LoadNextScene()
        {
            int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1);
        }

        public void LoadPreviousScene()
        {
            int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex > 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex - 1);
            }
        }

        public void LoadMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        // 비동기 씬 로딩을 위한 메서드 (로딩 화면이 필요한 경우)
        public AsyncOperation LoadSceneAsync(string sceneName)
        {
            return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        }
    }
}