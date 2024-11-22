using System;

namespace GearsAndDreams.Casting.Interfaces
{
    public interface IBucketController
    {
        float BucketAngle { get; }
        bool IsTilted { get; }
        public float BaseRotationAngle { get; }
        public float BucketLocalRotationAngle { get; }
        void UpdateTilt(float value);
        event Action<float> OnBucketTilted;
    }
}