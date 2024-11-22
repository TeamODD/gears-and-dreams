using System;
using GearsAndDreams.Casting.Enums;

namespace GearsAndDreams.Casting.Interfaces
{
    public interface IGameController
    {
        GameState CurrentState { get; }
        float TargetHeight { get; }
        event Action<GameState> OnGameStateChanged;
        event Action<AccuracyLevel, int> OnScoreCalculated;
        void StartGame();
        void EvaluateAccuracy(float currentHeight);
    }
}