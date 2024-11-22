using System;
using System.Collections;
using GearsAndDreams.Casting.Configuration;
using GearsAndDreams.Casting.Enums;
using GearsAndDreams.Casting.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace GearsAndDreams.Casting
{
    public class CastingGameManager : MonoBehaviour, IGameController
    {
        [SerializeField] private CastingGameSettings settings;
        [SerializeField] private TargetLine targetLine;
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private Transform lavaTopPoint;

        private float _previousScrollValue;
        private bool _isEvaluated;
        public GameState CurrentState { get; private set; }
        public float TargetHeight { get; private set; }
        public event Action<GameState> OnGameStateChanged;
        public event Action<AccuracyLevel, int> OnScoreCalculated;

        private void Start()
        {
            CurrentState = GameState.Ready;
            targetLine.SetActive(false);
        }

        private void Update()
        {
            if (CurrentState != GameState.Playing || _isEvaluated) return;

            float currentScrollValue = scrollbar.value;
            if (currentScrollValue < _previousScrollValue)
            {
                _isEvaluated = true;
                StartCoroutine(EvaluateWithDelay());
            }
            _previousScrollValue = currentScrollValue;
        }

        private IEnumerator EvaluateWithDelay()
        {
            yield return new WaitForSeconds(2f);  // 2초 딜레이
            EvaluateAccuracy(lavaTopPoint.position.y);
        }

        public void StartGame()
        {
            if (CurrentState != GameState.Ready) return;

            GenerateNewTarget();

            _isEvaluated = false;
            _previousScrollValue = scrollbar.value;
            CurrentState = GameState.Playing;
            OnGameStateChanged?.Invoke(CurrentState);
        }

        private void GenerateNewTarget()
        {
            TargetHeight = UnityEngine.Random.Range(settings.MinTargetHeight, settings.MaxTargetHeight);
            targetLine.SetHeight(TargetHeight);
            targetLine.SetActive(true);
        }

        public void EvaluateAccuracy(float currentHeight)
        {
            if (CurrentState != GameState.Playing) return;

            CurrentState = GameState.Evaluating;
            OnGameStateChanged?.Invoke(CurrentState);

            Debug.Log($"Lava Height: {currentHeight}");
            Debug.Log($"Target Height: {TargetHeight}");

            // 목표 높이와의 차이를 백분율로 계산
            float difference = Mathf.Abs(currentHeight - TargetHeight);
            float percentageDiff = difference / TargetHeight;

            AccuracyLevel accuracy;
            int score = settings.BaseScore;

            if (percentageDiff <= settings.ThresholdTen)
            {
                accuracy = AccuracyLevel.Perfect;
                // 10% 이하는 감점 없음
            }
            else if (percentageDiff <= settings.ThresholdTwenty)
            {
                accuracy = AccuracyLevel.Good;
                score -= settings.PenaltyTwenty;
            }
            else if (percentageDiff <= settings.ThresholdThirty)
            {
                accuracy = AccuracyLevel.Fair;
                score -= settings.PenaltyThirty;
            }
            else if (percentageDiff <= settings.ThresholdForty)
            {
                accuracy = AccuracyLevel.Poor;
                score -= settings.PenaltyForty;
            }
            else
            {
                accuracy = AccuracyLevel.Miss;
                score -= settings.PenaltyOver;
            }

            Debug.Log($"Difference: {percentageDiff * 100:F1}%");
            Debug.Log($"Score: {score}");

            OnScoreCalculated?.Invoke(accuracy, score);

            StartCoroutine(FinishGameRoutine());
        }

        private IEnumerator FinishGameRoutine()
        {
            yield return new WaitForSeconds(2f);

            CurrentState = GameState.Finished;
            OnGameStateChanged?.Invoke(CurrentState);
            targetLine.SetActive(false);
        }
    }
}