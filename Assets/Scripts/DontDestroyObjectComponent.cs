namespace Assets.Scripts
{
    using UnityEngine;

    public class DontDestroyObjectComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}