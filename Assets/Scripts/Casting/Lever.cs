using GearsAndDreams.Casting.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GearsAndDreams.Casting
{
    public class Lever : MonoBehaviour
    {
        private Scrollbar scrollbar;
        [SerializeField] private Bucket bucketComponent;
        [SerializeField] private Lava lavaComponent;
        [SerializeField] private GameObject upLever;
        [SerializeField] private GameObject downLever;

        private IBucketController _bucket;
        private ILavaController _lava;

        private void Awake()
        {
            scrollbar = GetComponent<Scrollbar>();


            InitializeComponents();
            SetupEventListeners();
            UpdateLeverState(0f);
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
                scrollbar.onValueChanged.AddListener(UpdateLeverState);
            }
        }

        private void UpdateLeverState(float value)
        {
            if (value >= 0.99f)
            {
                upLever.SetActive(false);
                downLever.SetActive(true);
            }
            else if (value <= 0.01f)
            {
                upLever.SetActive(true);
                downLever.SetActive(false);
            }
            else
            {
                upLever.SetActive(false);
                downLever.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            if (scrollbar != null && _bucket != null)
            {
                scrollbar.onValueChanged.RemoveListener(_bucket.UpdateTilt);
                scrollbar.onValueChanged.RemoveListener(UpdateLeverState);
            }
        }
    }
}