using GearsAndDreams.Casting.Enums;
using TMPro;
using UnityEngine;

namespace GearsAndDreams.Casting
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private CastingGameManager castingGameManager;
        [SerializeField] private TextMeshProUGUI accuracyText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject resultPanel;

        private void Start()
        {
            castingGameManager.OnScoreCalculated += HandleScoreCalculated;
            castingGameManager.OnGameStateChanged += HandleGameStateChanged;
            HideResult();
        }

        private void OnDestroy()
        {
            castingGameManager.OnScoreCalculated -= HandleScoreCalculated;
            castingGameManager.OnGameStateChanged -= HandleGameStateChanged;
        }

        private void HandleScoreCalculated(AccuracyLevel accuracy, int score)
        {
            accuracyText.text = accuracy.ToString();
            scoreText.text = score.ToString();
            ShowResult();
        }

        private void HandleGameStateChanged(GameState newState)
        {
            if (newState == GameState.Ready)
            {
                HideResult();
            }
        }

        private void ShowResult()
        {
            // resultPanel.SetActive(true);
        }

        private void HideResult()
        {
            resultPanel.SetActive(false);
        }
    }
}