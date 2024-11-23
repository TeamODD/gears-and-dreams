using UnityEngine;
using DG.Tweening;
using GearsAndDreams.Polishing.Configuration;

namespace GearsAndDreams.Polishing
{
    public class GearColorChanger : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private int lastRotationCount = 0;
        private Color originalColor;

        public CircleDetector circleDetector;
        public PolishingGameSettings gameSettings;

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

                if (circleDetector.RotationCount <= gameSettings.PERFECT_COUNT)
                {
                    // 0-20: 원래 색상에서 흰색으로
                    float t = Mathf.Clamp01((float)circleDetector.RotationCount / gameSettings.PERFECT_COUNT);
                    targetColor = Color.Lerp(originalColor, Color.white, t);
                }
                else if (circleDetector.RotationCount <= gameSettings.MAX_COUNT)
                {
                    // 20-60: 흰색에서 회색으로
                    float t = Mathf.Clamp01((float)(circleDetector.RotationCount - gameSettings.PERFECT_COUNT) / (gameSettings.MAX_COUNT - gameSettings.PERFECT_COUNT));
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
