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
        public float PerfectThreshold = 0.05f;  // 완벽한 정확도로 판정할 오차 범위
        public float GoodThreshold = 0.1f; // 좋음으로 판정할 오차 범위
        
        [Header("점수")]
        public int PerfectScore = 100;
        public int GoodScore = 50;
        public int PoorScore = 20;
    }
}
