using System;
using System.Collections;
using GearsAndDreams.Casting.Configuration;
using GearsAndDreams.Casting.Enums;
using GearsAndDreams.Casting.Interfaces;
using UnityEngine;

namespace GearsAndDreams.Casting
{
    public class CastingGameManager : MonoBehaviour, IGameController
    {
        [SerializeField] private CastingGameSettings settings;
        [SerializeField] private TargetLine targetLine;

        public GameState CurrentState { get; private set; }
        public float TargetHeight { get; private set; }
        public event Action<GameState> OnGameStateChanged;
        public event Action<AccuracyLevel, int> OnScoreCalculated;

        private void Start()
        {
            CurrentState = GameState.Ready;
            targetLine.SetActive(false);
        }

        public void StartGame()
        {
            if (CurrentState != GameState.Ready) return;

            GenerateNewTarget();

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