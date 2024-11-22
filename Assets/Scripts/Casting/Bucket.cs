using System;
using DG.Tweening;
using UnityEngine;

namespace GearsAndDreams.Casting
{
    public class Bucket : MonoBehaviour
    {
        public float maxRotationAngle = 90f;
        [SerializeField] private Ease easeType = Ease.Linear;

        public event Action<float> OnBucketTilted;

        public float BucketAngle { get; private set; }
        

        public bool IsTilted => BucketAngle > 0.01f;

        public void UpdateTilt(float value)
        {
            float newAngle = Mathf.Lerp(30, 30 + maxRotationAngle, value);

            // 변경이 있는 경우에만 애니메이션 호출
            if (Math.Abs(BucketAngle - newAngle) > 0.01f)
            {
                BucketAngle = newAngle;
                transform.DORotate(new Vector3(0f, 0f, BucketAngle), 3f).SetEase(easeType);
                OnBucketTilted?.Invoke(BucketAngle);
            }
        }
    }
}