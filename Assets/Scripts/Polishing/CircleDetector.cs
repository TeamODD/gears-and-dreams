using UnityEngine;

namespace GearsAndDreams.Polishing
{
    public class CircleDetector : MonoBehaviour
    {
        public SpriteRenderer targetSpriteRenderer;

        private CircleDrawing circleDrawing;
        private int rotationCount = 0;
        private bool isDrawing = false;

        void Start()
        {
            circleDrawing = new CircleDrawing();
            if (targetSpriteRenderer == null)
            {
                Debug.LogWarning("TargetSprite가 지정되지 않았습니다!");
            }
        }

        void Update()
        {
            HandleMouseInput();
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartDrawing();
            }
            else if (Input.GetMouseButton(0))
            {
                UpdateDrawing();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndDrawing();
            }
        }

        void StartDrawing()
        {
            if (!IsMouseOverTargetSprite()) return;

            circleDrawing.Reset();
            isDrawing = true;
            Vector2 firstPoint = Input.mousePosition;
            circleDrawing.AddPoint(firstPoint);
        }

        void UpdateDrawing()
        {
            if (!isDrawing || !IsMouseOverTargetSprite()) return;

            Vector2 currentPoint = Input.mousePosition;

            if (circleDrawing.ShouldAddPoint(currentPoint))
            {
                circleDrawing.AddPoint(currentPoint);

                if (circleDrawing.TryDetectFullRotation(currentPoint, out bool isRotationComplete))
                {
                    if (circleDrawing.IsWorldOriginInsideCircle())
                    {
                        rotationCount++;
                        Debug.Log($"회전 횟수: {rotationCount}");
                    }
                }
            }
        }

        void EndDrawing()
        {
            isDrawing = false;
            circleDrawing.Reset();
        }

        private bool IsMouseOverTargetSprite()
        {
            if (targetSpriteRenderer == null) return false;

            // 마우스 위치를 스크린 좌표에서 월드 좌표로 변환
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = targetSpriteRenderer.transform.position.z;

            // 스프라이트의 경계 박스 확인
            Bounds spriteBounds = targetSpriteRenderer.bounds;
            return spriteBounds.Contains(mouseWorldPos);
        }

        void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 600, 60), $"회전 횟수: {rotationCount}");
        }
    }
}
