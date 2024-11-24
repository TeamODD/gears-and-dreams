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
        // [SerializeField] private GameObject resultPanel;

        private void Start()
        {
            castingGameManager.OnScoreCalculated += HandleScoreCalculated;
            castingGameManager.OnGameStateChanged += HandleGameStateChanged;
            // HideResult();
        }

        private void OnDestroy()
        {
            castingGameManager.OnScoreCalculated -= HandleScoreCalculated;
            castingGameManager.OnGameStateChanged -= HandleGameStateChanged;
        }

        private void HandleScoreCalculated(AccuracyLevel accuracy, int score)
        {
            string accuracyMessage = GetAccuracyMessage(accuracy);
            accuracyText.text = accuracyMessage;
            scoreText.text = $"Score: {score}";
            // ShowResult();
        }

        private string GetAccuracyMessage(AccuracyLevel accuracy)
        {
            return accuracy switch
            {
                AccuracyLevel.Perfect => "Perfect!\n±10%",
                AccuracyLevel.Good => "Good\n±20%",
                AccuracyLevel.Fair => "Fair\n±30%",
                AccuracyLevel.Poor => "Poor\n±40%",
                AccuracyLevel.Miss => "Miss\n±40% over",
                _ => accuracy.ToString()
            };
        }

        private void HandleGameStateChanged(GameState newState)
        {
            if (newState == GameState.Ready)
            {
                // HideResult();
            }
        }

        // private void ShowResult()
        // {
        //     resultPanel.SetActive(true);
        // }

        // private void HideResult()
        // {
        //     resultPanel.SetActive(false);
        // }
    }
}