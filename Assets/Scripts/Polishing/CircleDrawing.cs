using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GearsAndDreams.Polishing
{
    public class CircleDrawing
    {
        public List<Vector2> Points { get; private set; }
        public Vector2 CenterPoint { get; private set; }
        private float MinDistance { get; }
        private float AngleSum { get; set; }
        private float PreviousAngle { get; set; }

        public CircleDrawing(float minDistance = 10f)
        {
            Points = new List<Vector2>();
            MinDistance = minDistance;
        }

        public bool ShouldAddPoint(Vector2 currentPoint)
        {
            return Points.Count == 0 ||
                   Vector2.Distance(currentPoint, Points[^1]) > MinDistance;
        }

        public void AddPoint(Vector2 point)
        {
            Points.Add(point);
            UpdateCenterPoint();
        }

        private void UpdateCenterPoint()
        {
            CenterPoint = Points.Aggregate(Vector2.zero, (sum, point) => sum + point) / Points.Count;
        }

        public float CalculateAngle(Vector2 point)
        {
            Vector2 direction = point - CenterPoint;
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

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

        public bool IsWorldOriginInsideCircle()
        {
            // Calculate radius
            float maxDistance = Points.Max(point => Vector2.Distance(CenterPoint, point));

            // Convert world origin to screen point
            Vector3 worldOriginScreen = Camera.main.WorldToScreenPoint(Vector3.zero);

            // Check if world origin is inside the circle
            return Vector2.Distance(CenterPoint, new Vector2(worldOriginScreen.x, worldOriginScreen.y)) <= maxDistance;
        }

        public void Reset()
        {
            Points.Clear();
            AngleSum = 0f;
        }
    }
}
