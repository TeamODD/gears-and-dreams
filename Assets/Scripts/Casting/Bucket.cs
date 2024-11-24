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
        [SerializeField] private GameObject LavaFallLarge;
        [SerializeField] private GameObject LavaFallMedium;
        [SerializeField] private GameObject LavaFallSmall;

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
            LavaFallLarge.SetActive(false);
            LavaFallMedium.SetActive(false);
            LavaFallSmall.SetActive(false);

            if (settings == null)
            {
                Debug.LogError("Bucket Settings이 없음");
            }
        }

        private void Update()
        {
            if (BucketLocalRotationAngle <= 31f)
            {
                LavaFallLarge.SetActive(false);
                LavaFallMedium.SetActive(false);
                LavaFallSmall.SetActive(false);
            }
            else if (BucketLocalRotationAngle > 30f && BucketLocalRotationAngle <= 50f)
            {
                LavaFallLarge.SetActive(false);
                LavaFallMedium.SetActive(false);
                LavaFallSmall.SetActive(true);
            }
            else if (BucketLocalRotationAngle > 50f && BucketLocalRotationAngle <= 90f)
            {
                LavaFallLarge.SetActive(false);
                LavaFallMedium.SetActive(true);
                LavaFallSmall.SetActive(false);
            }
            else if (BucketLocalRotationAngle > 90f)
            {
                LavaFallLarge.SetActive(true);
                LavaFallMedium.SetActive(false);
                LavaFallSmall.SetActive(false);
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