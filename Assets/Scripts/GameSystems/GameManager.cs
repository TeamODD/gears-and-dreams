using System;
using System.Collections;
using System.Collections.Generic;
using GearsAndDreams.Casting;
using GearsAndDreams.Polishing;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GearsAndDreams.GameSystems.SoundManager;

namespace GearsAndDreams.GameSystems
{
    public class GameManager : Singleton<GameManager>
    {
        public enum GameType
        {
            Start,
            MaterialSelection,
            Casting,
            Polishing,
            Cutting,
            Receipt
        }
        private GameType _currentGameType;
        public GameType CurrentGameType
        {
            get => _currentGameType;
            set
            {
                _currentGameType = value;
                if (value == GameType.MaterialSelection)
                {
                    currentDay++;
                }
                PlayBGMForGameType(value);
                StartCoroutine(LoadSceneWithDelay((int)value));
            }
        }
        public void ChangeGame()
        {
            GameType nextGameType = (GameType)((int)(_currentGameType + 1) % 6);
            if (nextGameType == 0)
            {
                nextGameType++;
            }
            CurrentGameType = nextGameType;
        }

        public int CurrentDay => currentDay;
        public int TotalScore => totalScore;

        public GameObject ESCSetting;

        private Dictionary<GameType, int> gameScores;
        private List<int> dailyTotalScores;
        private int currentDay = 0;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Start Scene")
            {
                ESCSetting.SetActive(!ESCSetting.activeSelf);
                Time.timeScale = ESCSetting.activeSelf ? 0f : 1f;
                Debug.Log("ESC Setting");
            }
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void ResetGame()
        {
            currentDay = 1;
            totalScore = 0;
            dailyTotalScores.Clear();
            ResetDailyScores();
            CurrentGameType = GameType.Start;
        }

        private IEnumerator LoadSceneWithDelay(int sceneIndex)
        {
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene(sceneIndex);
        }

        private void PlayBGMForGameType(GameType gameType)
        {
            if (SoundManager.Instance == null)
            {
                return;
            }

            Debug.Log($"Playing BGM for game type: {gameType}");

            switch (gameType)
            {
                case GameType.MaterialSelection:
                    Debug.Log("Playing 스테이지1and4 BGM");
                    SoundManager.Instance.StopAllSoundsOfType(SoundType.BGM);
                    SoundManager.Instance.Play("스테이지1and4");
                    break;
                case GameType.Casting:
                    Debug.Log("Playing 스테이지2 BGM");
                    SoundManager.Instance.StopAllSoundsOfType(SoundType.BGM);
                    SoundManager.Instance.Play("스테이지2");
                    break;
                case GameType.Polishing:
                    Debug.Log("Playing 스테이지3 BGM");
                    SoundManager.Instance.StopAllSoundsOfType(SoundType.BGM);
                    SoundManager.Instance.Play("스테이지3");
                    break;
                case GameType.Cutting:
                    Debug.Log("Playing 스테이지1and4 BGM");
                    SoundManager.Instance.StopAllSoundsOfType(SoundType.BGM);
                    SoundManager.Instance.Play("스테이지1and4");
                    break;
            }
        }
    }
}
