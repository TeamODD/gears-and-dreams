using GearsAndDreams.Polishing;
using UnityEngine;
using DG.Tweening;

namespace GearsAndDreams.Polishing
{
    public class GearColorChanger : MonoBehaviour
    {
        public CircleDetector circleDetector;
        private SpriteRenderer spriteRenderer;
        private int lastRotationCount = 0;
        private Color originalColor;

        private const int FIRST_PHASE_MAX = 20;
        private const int SECOND_PHASE_MAX = 60;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalColor = spriteRenderer.color;
        }

        private void Update()
        {
            if (circleDetector.RotationCount > lastRotationCount)
            {
                lastRotationCount = circleDetector.RotationCount;
                Color targetColor;

                if (circleDetector.RotationCount <= FIRST_PHASE_MAX)
                {
                    // 0-20: 원래 색상에서 흰색으로
                    float t = Mathf.Clamp01((float)circleDetector.RotationCount / FIRST_PHASE_MAX);
                    targetColor = Color.Lerp(originalColor, Color.white, t);
                }
                else if (circleDetector.RotationCount <= SECOND_PHASE_MAX)
                {
                    // 20-60: 흰색에서 회색으로
                    float t = Mathf.Clamp01((float)(circleDetector.RotationCount - FIRST_PHASE_MAX) / (SECOND_PHASE_MAX - FIRST_PHASE_MAX));
                    targetColor = Color.Lerp(Color.white, Color.gray, t);
                }
                else
                {
                    targetColor = Color.gray;
                }

                spriteRenderer.DOColor(targetColor, 0.5f);
            }
        }
    }
}
