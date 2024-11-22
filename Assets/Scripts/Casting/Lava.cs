using UnityEngine;

namespace GearsAndDreams.Casting
{
    public class Lava : MonoBehaviour
    {
        [SerializeField] private float scaleMultiplier = 0.1f; // 증가 속도 계수
        [SerializeField] private float maxScale = 2f;          // 최대 스케일 제한
        private const float BucketBaseRotation = 30f;          // Bucket 기본 회전값

        private Transform _bucketTransform;

        public void Initialize(Transform bucketTransform)
        {
            _bucketTransform = bucketTransform;
        }

        private void Update()
        {
            if (_bucketTransform != null)
            {
                float bucketRotationZ = _bucketTransform.localEulerAngles.z;

                // 기본값(30)을 기준으로 회전 차이 계산
                bucketRotationZ = Mathf.Abs(bucketRotationZ - BucketBaseRotation);

                // 180도를 초과할 경우 올바른 범위로 변환
                bucketRotationZ = bucketRotationZ > 180 ? 360 - bucketRotationZ : bucketRotationZ;

                if (bucketRotationZ > 0.01f) // 기울어진 상태에서만 증가
                {
                    Vector3 targetScale = transform.localScale;
                    targetScale.y += bucketRotationZ * scaleMultiplier * Time.deltaTime;

                    // 최대 스케일 제한
                    targetScale.y = Mathf.Clamp(targetScale.y, 0, maxScale);

                    transform.localScale = targetScale;
                }
            }
        }
    }
}