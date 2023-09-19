using UnityEngine;

namespace Game
{
    public readonly struct AimData
    {
        public readonly IEntityExt target;

        public readonly Vector3 targetPosition;

        public readonly Vector3 self;

        public readonly float dist;

        public AimData(IEntityExt target, Vector3 targetPosition, Vector3 self, float dist)
        {
            this.target = target;
            this.targetPosition = targetPosition;
            this.self = self;
            this.dist = dist;
        }
    }
}