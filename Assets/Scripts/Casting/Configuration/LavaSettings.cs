using UnityEngine;

namespace GearsAndDreams.Casting.Configuration
{
    [CreateAssetMenu(fileName = "LavaSettings", menuName = "Scriptable Objects/LavaSettings")]
    public class LavaSettings : ScriptableObject
    {
        public float ScaleMultiplier = 0.1f;
        public float MaxScale = 2f;
    }
}
