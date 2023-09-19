using System;
using Game.Data;
using UnityEngine;

namespace Game.Items
{
    public interface IWeapon : IEquipment
    {
        float Cooldown { get; }

        new IWeaponData Data { get; }

        event Action<DamageHitData> OnHit;

        float Range { get; }

        void Use(Vector3 target);
    }

    public static class WeaponExtensions
    {
        public static bool IsReady(this IWeapon weapon)
        {
            return weapon.Cooldown <= 0;
        }
    }
}