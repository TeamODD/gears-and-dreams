using UnityEngine;

namespace GearsAndDreams.Casting.Configuration
{
    [CreateAssetMenu(fileName = "CastingGameSettings", menuName = "Scriptable Objects/CastingGameSettings")]
    public class CastingGameSettings : ScriptableObject
    {
        [Header("목표 라인 설정")]
        public float MinTargetHeight = 0.5f;
        public float MaxTargetHeight = 1.8f;

        [Header("점수 산정 세팅")]
        // public float PerfectThreshold = 0.05f;  // 완벽한 정확도로 판정할 오차 범위
        // public float GoodThreshold = 0.1f; // 좋음으로 판정할 오차 범위
        public float ThresholdTen = 0.1f;     // ±10% 임계값
        public float ThresholdTwenty = 0.2f;   // ±20% 임계값
        public float ThresholdThirty = 0.3f;   // ±30% 임계값
        public float ThresholdForty = 0.4f;    // ±40% 임계값

        [Header("점수")]
        // public int PerfectScore = 100;
        // public int GoodScore = 50;
        // public int PoorScore = 20;
        public int BaseScore = 1000;           // 기본 점수
        public int PenaltyTwenty = 250;        // ±20% 이하 감점
        public int PenaltyThirty = 500;        // ±30% 이하 감점
        public int PenaltyForty = 750;         // ±40% 이하 감점
        public int PenaltyOver = 1000;         // ±40% 초과 감점
    }
}
