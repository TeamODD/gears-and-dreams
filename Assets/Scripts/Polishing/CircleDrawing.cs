using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GearsAndDreams.Polishing
{
    /// <summary>
    /// 원형 그림을 관리하고 감지하는 책임을 가진 클래스입니다.
    /// </summary>
    public class CircleDrawing
    {
        // 원을 구성하는 점들의 목록.
        public List<Vector2> Points { get; private set; }

        // 점들에 기반하여 계산된 원의 중심 점.
        public Vector2 CenterPoint { get; private set; }

        // 새로운 점을 추가하는 데 필요한 최소 거리.
        private float MinDistance { get; }

        // 각 점이 지나간 각도의 합.
        private float AngleSum { get; set; }

        // 이전 각도를 저장하는 필드. 다음 각도를 계산하는 데 사용.
        private float PreviousAngle { get; set; }

        /// <summary>
        /// 새로운 CircleDrawing 인스턴스를 초기화.
        /// </summary>
        /// <param name="minDistance">새로운 점을 추가하는 데 필요한 최소 거리.</param>
        public CircleDrawing(float minDistance = 10f)
        {
            Points = new List<Vector2>();
            MinDistance = minDistance;
        }

        /// <summary>
        /// 현재 점이 원에 추가되어야 하는지 확인.
        /// </summary>
        /// <param name="currentPoint">현재 점.</param>
        /// <returns>점이 원에 추가되어야 하면 true, 아니라면 false.</returns>
        public bool ShouldAddPoint(Vector2 currentPoint)
        {
            return Points.Count == 0 ||
                   Vector2.Distance(currentPoint, Points[^1]) > MinDistance;
        }

        /// <summary>
        /// 새로운 점을 원에 추가.
        /// </summary>
        /// <param name="point">새로운 점.</param>
        public void AddPoint(Vector2 point)
        {
            Points.Add(point);
            UpdateCenterPoint();
        }

        /// <summary>
        /// 점들에 기반하여 원의 중심 점을 업데이트.
        /// </summary>
        private void UpdateCenterPoint()
        {
            CenterPoint = Points.Aggregate(Vector2.zero, (sum, point) => sum + point) / Points.Count;
        }

        /// <summary>
        /// 점의 각도를 계산.
        /// </summary>
        /// <param name="point">점.</param>
        /// <returns>점의 각도 (단위: 도).</returns>
        public float CalculateAngle(Vector2 point)
        {
            Vector2 direction = point - CenterPoint;
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// 원이 완전히 회전했는지 확인.
        /// </summary>
        /// <param name="currentPoint">현재 점.</param>
        /// <param name="isRotationComplete">원이 완전히 회전했는지 나타내는 출력 파라미터.</param>
        /// <returns>원이 완전히 회전했으면 true, 아니라면 false.</returns>
        public bool TryDetectFullRotation(Vector2 currentPoint, out bool isRotationComplete)
        {
            isRotationComplete = false;

            if (Points.Count <= 3) return false;

            float currentAngle = CalculateAngle(currentPoint);
            float deltaAngle = Mathf.DeltaAngle(PreviousAngle, currentAngle);
            AngleSum += deltaAngle;

            if (Mathf.Abs(AngleSum) >= 360f)
            {
                isRotationComplete = true;
                AngleSum = 0f;
            }

            PreviousAngle = currentAngle;
            return isRotationComplete;
        }

        /// <summary>
        /// 원점 (0, 0, 0) 이 원 안에 있는지 확인.
        /// </summary>
        /// <returns>월드 원점이 원 안에 있으면 true, 아니라면 false.</returns>
        public bool IsWorldOriginInsideCircle()
        {
            // 반지름 계산
            float maxDistance = Points.Max(point => Vector2.Distance(CenterPoint, point));

            // 원점을 스크린 점으로 변환
            Vector3 worldOriginScreen = Camera.main.WorldToScreenPoint(Vector3.zero);

            // 원점이 원 안에 있는지 확인
            return Vector2.Distance(CenterPoint, worldOriginScreen) <= maxDistance;
        }

        /// <summary>
        /// 원을 초기화.
        /// </summary>
        public void Reset()
        {
            Points.Clear();
            AngleSum = 0f;
        }
    }
}
