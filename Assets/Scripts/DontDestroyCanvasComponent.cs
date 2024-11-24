namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class DontDestroyCanvasComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded+=GetMainCamera;
        }
        private void GetMainCamera(Scene scene, LoadSceneMode mode)
        {
            GetComponent<Canvas>().worldCamera=Camera.main;
        }
    }
}