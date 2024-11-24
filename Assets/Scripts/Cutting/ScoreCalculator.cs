namespace Assets.Scripts.Cutting
{
    using GearsAndDreams.GameSystems;
    using UnityEngine;
    using UnityEngine.Events;

    public class ScoreCalculator : MonoBehaviour
    {
        private int _score;
        private void Awake()
        {
            _score=1000;    
        }
        public void ReduceScore()
        {
            _score-=125;
            print("Reduced Socre! Current Score: "+_score);
        }
        public void SetScore()
        {
            GameManager.Instance?.SetGameScore(GameManager.GameType.Cutting, _score);
        }
    }
}