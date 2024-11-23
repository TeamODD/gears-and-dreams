using System;
using DG.Tweening;
using GearsAndDreams.Casting.Configuration;
using GearsAndDreams.Casting.Interfaces;
using GearsAndDreams.GameSystems;
using UnityEngine;

namespace GearsAndDreams.Casting
{
    public class Bucket : MonoBehaviour, IBucketController
    {
        [SerializeField] private BucketSettings settings;

        public float BaseRotationAngle => settings.BaseRotationAngle;

        public float BucketLocalRotationAngle
        {
            get => transform.localRotation.eulerAngles.z;
        }

        public event Action<float> OnBucketTilted;

        public float BucketAngle { get; private set; }
        public bool IsTilted => BucketAngle > 0.01f;

        private void Awake()
        {
            if (settings == null)
            {
                Debug.LogError("Bucket Settings이 없음");
            }
        }

        public void UpdateTilt(float value)
        {
            if (settings == null) return;

            float newAngle = Mathf.Lerp(
                settings.BaseRotationAngle,
                settings.BaseRotationAngle + settings.MaxRotationAngle,
                value
            );

            if (Math.Abs(BucketAngle - newAngle) > 0.01f)
            {
                BucketAngle = newAngle;
                AnimateBucketRotation();
                OnBucketTilted?.Invoke(BucketAngle);
            }
        }

        private void AnimateBucketRotation()
        {
            // SoundManager.Instance.Play("쇳물");

            transform.DORotate(
                new Vector3(0f, 0f, BucketAngle),
                settings.AnimationDuration
            ).SetEase(settings.EaseType);
        }
    }
}