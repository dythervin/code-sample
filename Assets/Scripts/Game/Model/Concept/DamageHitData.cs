using Game.Common;
using UnityEngine;

namespace Game.Items
{
    public readonly struct DamageHitData
    {
        public readonly RaycastHit raycastHit;

        public readonly IEntityExt target;

        public readonly Damage damage;

        public DamageHitData(RaycastHit raycastHit, IEntityExt target, Damage damage)
        {
            this.raycastHit = raycastHit;
            this.target = target;
            this.damage = damage;
        }
    }
}