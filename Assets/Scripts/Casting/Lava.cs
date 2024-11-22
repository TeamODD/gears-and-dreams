using GearsAndDreams.Casting.Configuration;
using GearsAndDreams.Casting.Interfaces;
using UnityEngine;

namespace GearsAndDreams.Casting
{
    public class Lava : MonoBehaviour, ILavaController
    {
        [SerializeField] private LavaSettings settings;
        
        private IBucketController _bucketController;
        private float _baseRotation;

        public void Initialize(IBucketController bucketController)
        {
            _bucketController = bucketController;
            if (settings == null)
            {
                Debug.LogError("Lava Setting이 없음");
            }
        }

        private void Update()
        {
            if (_bucketController == null || settings == null) return;

            if (_bucketController.IsTilted)
            {
                UpdateLavaScale(_bucketController.BucketLocalRotationAngle);
            }
        }

        public void UpdateLavaScale(float bucketAngle)
        {
            float rotationDelta = Mathf.Abs(bucketAngle - _bucketController.BaseRotationAngle);
            rotationDelta = rotationDelta > 180 ? 360 - rotationDelta : rotationDelta;
            
            if (rotationDelta > 0.01f)
            {
                Vector3 targetScale = transform.localScale;
                targetScale.y += rotationDelta * settings.ScaleMultiplier * Time.deltaTime;
                targetScale.y = Mathf.Clamp(targetScale.y, 0, settings.MaxScale);
                transform.localScale = targetScale;
            }
        }
    }
}