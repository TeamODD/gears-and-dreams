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
            EvaluateAccuracy(FindObjectOfType<Lava>().transform.localScale.y);
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

            float difference = Mathf.Abs(currentHeight - TargetHeight);
            AccuracyLevel accuracy;
            int score;

            if (difference <= settings.PerfectThreshold)
            {
                accuracy = AccuracyLevel.Perfect;
                score = settings.PerfectScore;
            }
            else if (difference <= settings.GoodThreshold)
            {
                accuracy = AccuracyLevel.Good;
                score = settings.GoodScore;
            }
            else
            {
                accuracy = AccuracyLevel.Poor;
                score = settings.GoodScore;
            }

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