using System;
using Game.Common;
using UnityEngine;

namespace Game.Items
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        public abstract event Action OnAttack;

        public abstract event Action OnCollision;

        public abstract event AttackDelegate OnLaunch;

        public abstract void Attack(in Vector3 from, in Vector3 vector3, in Damage attack1);
    }
}