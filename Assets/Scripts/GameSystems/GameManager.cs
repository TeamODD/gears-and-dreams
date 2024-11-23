using System;
using System.Collections.Generic;
using GearsAndDreams.Casting;
using GearsAndDreams.Polishing;
using UnityEngine;

namespace GearsAndDreams.GameSystems
{
    public class GameManager : Singleton<GameManager>
    {
        public enum GameType
        {
            Casting,
            MaterialSelection,
            Polishing,
            // 새로운 게임 추가 시 여기에 열거형 멤버 추가
        }

        public int CurrentDay => currentDay;
        public int TotalScore => totalScore;

        private Dictionary<GameType, int> gameScores;
        private List<int> dailyTotalScores;
        private int currentDay = 1;
        private int totalScore;

        private void Start()
        {
            gameScores = new Dictionary<GameType, int>();
            dailyTotalScores = new List<int>();

            foreach (GameType gameType in Enum.GetValues(typeof(GameType)))
            {
                gameScores[gameType] = 0;
            }
        }

        //각 게임에서 해당 함수 호출
        public void SetGameScore(GameType gameType, int score)
        {
            gameScores[gameType] = score;
            Debug.Log($"Day {currentDay} - {gameType} Score: {score}");
        }

        public int GetGameScore(GameType gameType)
        {
            return gameScores.TryGetValue(gameType, out int score) ? score : 0;
        }

        public void FinishDay()
        {
            int dailyTotal = 0;
            foreach (var score in gameScores.Values)
            {
                dailyTotal += score;
            }

            dailyTotalScores.Add(dailyTotal);
            Debug.Log($"Day {currentDay} Total Score: {dailyTotal}");

            if (currentDay < 3)
            {
                currentDay++;
                ResetDailyScores();
            }
            else
            {
                CalculateFinalScore();
            }
        }

        private void ResetDailyScores()
        {
            foreach (GameType gameType in Enum.GetValues(typeof(GameType)))
            {
                gameScores[gameType] = 0;
            }
        }

        private void CalculateFinalScore()
        {
            totalScore = 0;
            foreach (int dailyScore in dailyTotalScores)
            {
                totalScore += dailyScore;
            }

            Debug.Log($"Final Total Score after 3 days: {totalScore}");
            // DetermineEnding();
        }

        public void ResetGame()
        {
            currentDay = 1;
            totalScore = 0;
            dailyTotalScores.Clear();
            ResetDailyScores();
        }
    }
}
