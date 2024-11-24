using GearsAndDreams.GameSystems;
using GearsAndDreams.Polishing.Configuration;
using TMPro;
using UnityEngine;

namespace GearsAndDreams.Polishing
{
    public class PolishingGameManager : MonoBehaviour
    {
        public PolishingGameSettings settings;
        public CircleDetector circleDetector;
        public TMP_Text timeText;

        public float FinalScore => _finalScore;

        private float _timeLimit = PolishingGameSettings.TimeLimit;
        private int _finalScore;
        private bool _isGameOver = false;

        private void Update()
        {
            if (!_isGameOver)
            {
                if (_timeLimit > 0)
                {
                    Timer();
                }
                else
                {
                    EvaluateScore();
                    _isGameOver = true;
                }
            }
        }

        private void Timer()
        {
            _timeLimit -= Time.deltaTime;
            timeText.text = ((int)_timeLimit).ToString();
        }

        public void EvaluateScore()
        {
            _finalScore = settings.BaseScore;

            if (circleDetector.RotationCount >= 1 && circleDetector.RotationCount <= 19
            || circleDetector.RotationCount >= 50 && circleDetector.RotationCount <= 59) // 1-19, 50-59
            {
                _finalScore -= settings.PenaltyFar;
            }
            else if (circleDetector.RotationCount >= 10 && circleDetector.RotationCount <= 19
            || circleDetector.RotationCount >= 40 && circleDetector.RotationCount <= 49) // 10-19, 40-49
            {
                _finalScore -= settings.PenaltyMedium;
            }
            else if (circleDetector.RotationCount >= 20 && circleDetector.RotationCount <= 27
            || circleDetector.RotationCount >= 33 && circleDetector.RotationCount <= 39) // 20-27, 33-39
            {
                _finalScore -= settings.PenaltyNear;
            }
            else if (circleDetector.RotationCount == 60)
            {
                _finalScore = 0;
            }

            Debug.Log($"Final Score: {_finalScore}");
            GameManager.Instance.SetGameScore(GameManager.GameType.Polishing, _finalScore);
        }
    }
}
