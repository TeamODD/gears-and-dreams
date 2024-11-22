using System;
using GearsAndDreams.Casting.Configuration;
using GearsAndDreams.Casting.Enums;
using GearsAndDreams.Casting.Interfaces;
using UnityEngine;

namespace GearsAndDreams.Casting
{
    public class Lava : MonoBehaviour, ILavaController
    {
        [SerializeField] private LavaSettings settings;
        [SerializeField] private CastingGameManager castingGameManager;

        private IBucketController _bucketController;
        // private float _baseRotation;
        // private bool _isEvaluated;

        public void Initialize(IBucketController bucketController)
        {
            _bucketController = bucketController;
            if (settings == null || castingGameManager == null)
            {
                Debug.LogError("Lava Setting또는 CastingGameManager이 없음");
            }

            castingGameManager.OnGameStateChanged += HandleGameStateChanged;
        }

        private void OnDestroy()
        {
            if (castingGameManager != null)
            {
                castingGameManager.OnGameStateChanged -= HandleGameStateChanged;
            }
        }

        private void Update()
        {
            if (_bucketController == null || settings == null) return;

            if (_bucketController.IsTilted && castingGameManager.CurrentState == GameState.Playing)
            {
                UpdateLavaScale(_bucketController.BucketLocalRotationAngle);
                // CheckForEvaluation();
            }
        }

        private void HandleGameStateChanged(GameState newState)
        {
            if (newState == GameState.Ready)
            {
                ResetLava();
            }
        }

        private void ResetLava()
        {
            // 초기 위치로 리셋
            Vector3 position = transform.localPosition;
            position.y = -4.5f;
            transform.localPosition = position;
        }

        // private void CheckForEvaluation()
        // {
        //     if (!_isEvaluated &&
        //         transform.localScale.y >= settings.MaxScale) // && transform.localScale.y >= settings.MaxScale
        //     {
        //         _isEvaluated = true;
        //         castingGameManager.EvaluateAccuracy(transform.localScale.y);
        //     }
        // }

        public void UpdateLavaScale(float bucketAngle)
        {
            float rotationDelta = Mathf.Abs(bucketAngle - _bucketController.BaseRotationAngle);
            rotationDelta = rotationDelta > 180 ? 360 - rotationDelta : rotationDelta;

            if (rotationDelta > 0.01f)
            {
                Vector3 position = transform.localPosition;
                position.y += rotationDelta * settings.HeightMultiplier * Time.deltaTime;
                position.y = Mathf.Clamp(position.y, -7f, settings.MaxHeight);
                transform.localPosition = position;
            }
        }

        // public void UpdateLavaScale(float bucketAngle)
        // {
        //     float rotationDelta = Mathf.Abs(bucketAngle - _bucketController.BaseRotationAngle);
        //     rotationDelta = rotationDelta > 180 ? 360 - rotationDelta : rotationDelta;

        //     if (rotationDelta > 0.01f)
        //     {
        //         Vector3 targetScale = transform.localScale;
        //         targetScale.y += rotationDelta * settings.ScaleMultiplier * Time.deltaTime;
        //         targetScale.y = Mathf.Clamp(targetScale.y, 0, settings.MaxScale);
        //         transform.localScale = targetScale;
        //     }
        // }
    }
}