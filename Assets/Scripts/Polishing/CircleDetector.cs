using GearsAndDreams.Polishing.Configuration;
using UnityEngine;

namespace GearsAndDreams.Polishing
{
    public class CircleDetector : MonoBehaviour
    {
        public SpriteRenderer targetSpriteRenderer;
        public PolishingGameSettings gameSettings;


        private CircleDrawing circleDrawing;
        private int rotationCount = 0;
        private bool isDrawing = false;

        public int RotationCount
        {
            get { return rotationCount; }
            set
            {
                if (value < 0 || value > gameSettings.MAX_COUNT) return;
                rotationCount = value;
            }
        }


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

        /// <summary>
        /// 마우스 드래그를 시작할 때 호출되는 메서드입니다.
        /// </summary>
        void StartDrawing()
        {
            if (!IsMouseOverTargetSprite()) return;

            circleDrawing.Reset();
            isDrawing = true;
            Vector2 firstPoint = Input.mousePosition;
            circleDrawing.AddPoint(firstPoint);
        }

        /// <summary>
        /// 마우스 드래그 중 지속적으로 호출되는 메서드입니다.
        /// </summary>
        void UpdateDrawing()
        {
            if (!isDrawing || !IsMouseOverTargetSprite()) return;

            Vector2 currentPoint = Input.mousePosition;

            if (circleDrawing.ShouldAddPoint(currentPoint))
            {
                circleDrawing.AddPoint(currentPoint);

                // 완전한 회전이 감지되었는지 확인
                if (circleDrawing.TryDetectFullRotation(currentPoint, out bool isRotationComplete))
                {
                    // 원의 중심이 월드 원점 내부에 있는지 확인
                    if (circleDrawing.IsWorldOriginInsideCircle())
                    {
                        RotationCount++;
                        Debug.Log($"회전 횟수: {RotationCount}");
                    }
                }
            }
        }

        /// <summary>
        /// 마우스 드래그가 끝났을 때 호출되는 메서드입니다.
        /// </summary>
        void EndDrawing()
        {
            isDrawing = false;
            circleDrawing.Reset();
        }

        /// <summary>
        /// 마우스 커서가 대상 스프라이트 위에 있는지 확인하는 메서드입니다.
        /// </summary>
        /// <returns>마우스가 스프라이트 위에 있으면 true, 그렇지 않으면 false를 반환합니다.</returns>
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

        /// <summary>
        /// GUI를 그리는 메서드입니다. 회전 횟수를 화면에 표시합니다.
        /// </summary>
        // void OnGUI()
        // {
        //     GUI.Label(new Rect(10, 10, 600, 60), $"회전 횟수: {RotationCount}");
        // }
    }
}
