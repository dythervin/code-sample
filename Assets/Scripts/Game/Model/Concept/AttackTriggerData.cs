using UnityEngine;

namespace Game.Items
{
    public readonly struct AttackTriggerData
    {
        public readonly Vector3 muzzlePos;

        public readonly Vector3 dir;

        public AttackTriggerData(Vector3 muzzlePos, Vector3 dir)
        {
            this.muzzlePos = muzzlePos;
            this.dir = dir;
        }
    }
}