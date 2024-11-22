using System;

namespace GearsAndDreams.Casting.Interfaces
{
    public interface ILavaController
    {
        void UpdateLavaScale(float rotationDelta);
        void Initialize(IBucketController bucketController);
    }
}