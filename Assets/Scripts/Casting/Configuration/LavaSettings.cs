using UnityEngine;

namespace GearsAndDreams.Casting.Configuration
{
    [CreateAssetMenu(fileName = "LavaSettings", menuName = "Scriptable Objects/LavaSettings")]
    public class LavaSettings : ScriptableObject
    {
        public float HeightMultiplier = 0.1f;
        public float MaxHeight = 2f;
    }
}
