using UnityEngine;
using UnityEngine.UI;

namespace GearsAndDreams.Casting
{
    public class Lever : MonoBehaviour
    {
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private Bucket bucket;
        [SerializeField] private Lava lava;

        private void Awake()
        {
            if (scrollbar != null && bucket != null)
            {
                scrollbar.onValueChanged.AddListener(bucket.UpdateTilt);
                lava.Initialize(bucket.transform); // Bucket Transform 전달
            }
        }

        private void OnDestroy()
        {
            if (scrollbar != null && bucket != null)
            {
                scrollbar.onValueChanged.RemoveListener(bucket.UpdateTilt);
            }
        }
    }
}