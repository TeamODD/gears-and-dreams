using GearsAndDreams.Casting.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GearsAndDreams.Casting
{
    public class Lever : MonoBehaviour
    {
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private Bucket bucketComponent;
        [SerializeField] private Lava lavaComponent;

        private IBucketController _bucket;
        private ILavaController _lava;

        private void Awake()
        {
            InitializeComponents();
            SetupEventListeners();
        }

        private void InitializeComponents()
        {
            if (bucketComponent == null || lavaComponent == null || scrollbar == null)
            {
                Debug.LogError("컴포넌트가 배정되지 않았음.");
                return;
            }

            _bucket = bucketComponent;
            _lava = lavaComponent;
            _lava.Initialize(_bucket);
        }

        private void SetupEventListeners()
        {
            if (scrollbar != null && _bucket != null)
            {
                scrollbar.onValueChanged.AddListener(_bucket.UpdateTilt);
            }
        }

        private void OnDestroy()
        {
            if (scrollbar != null && _bucket != null)
            {
                scrollbar.onValueChanged.RemoveListener(_bucket.UpdateTilt);
            }
        }
    }
}