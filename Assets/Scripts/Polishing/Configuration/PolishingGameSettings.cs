using UnityEngine;

namespace GearsAndDreams.Polishing.Configuration
{
    [CreateAssetMenu(fileName = "PolishingGameSettings", menuName = "Scriptable Objects/PolishingGameSettings")]
    public class PolishingGameSettings : ScriptableObject
    {
        [Header("점수")]
        public int BaseScore = 1000; // 기본 점수, 30
        public int PenaltyFar = 750; //1 ~ 19, 50 ~ 59
        public int PenaltyMedium = 500; //10 ~ 19, 40 ~ 49
        public int PenaltyNear = 250; //20 ~ 29, 31 ~ 39


        [Header("회전 횟수 기준치")]
        public int PERFECT_COUNT = 30;
        public int MAX_COUNT = 60;

        [Header("제한 시간")]
        public static float TimeLimit = 30f;
    }
}
