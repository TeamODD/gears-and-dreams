using DG.Tweening;
using UnityEngine;

namespace GearsAndDreams.Casting.Configuration
{
    [CreateAssetMenu(fileName = "BucketSettings", menuName = "Scriptable Objects/BucketSettings")]
    public class BucketSettings : ScriptableObject
    {
        public float MaxRotationAngle = 90f;
        public float BaseRotationAngle = 30f;
        public float AnimationDuration = 3f;
        public Ease EaseType = Ease.Linear;
    }
}
