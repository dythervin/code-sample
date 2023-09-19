using Game.Data;
using Game.Items;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Common
{
    public readonly struct Damage
    {
        public readonly float amount;

        public readonly bool isCritical;

        public readonly DamageType type;

        [CanBeNull] public readonly IEntityExt source;

        public readonly float force;

        public Damage(float amount, bool isCritical, DamageType type, float force, IEntityExt source)
        {
            this.amount = amount;
            this.isCritical = isCritical;
            this.type = type;
            this.source = source;
            this.force = force;
        }

        public Damage(in Damage damage, float amount) : this(amount,
            damage.isCritical,
            damage.type,
            damage.force,
            damage.source)
        {
        }

        public override string ToString()
        {
            return
                $"{amount} {type} {(isCritical ? "[crit]" : null)} {(source != null ? $"Source: {source.Id}" : null)}";
        }
    }

    public static class WeaponDataExt
    {
        // public static readonly IReadOnlyList<WeaponType> AllTypes = (WeaponType[])Enum.GetValues(typeof(WeaponType));
        // public static readonly IReadOnlyList<WeaponSlots> AllSlots =
        //     (WeaponSlots[])Enum.GetValues(typeof(WeaponSlots));
        //
        // public static bool IsCompatible(WeaponType weaponType, WeaponSlots slot)
        // {
        //     if (slot == WeaponSlots.LeftForearm || weaponType == WeaponType.Shield)
        //         return weaponType == WeaponType.Shield && slot == WeaponSlots.LeftForearm;
        //
        //     switch (weaponType)
        //     {
        //         case WeaponType.Sword:
        //             return slot != WeaponSlots.None;
        //         case WeaponType.None:
        //             return true;
        //         case WeaponType.Spear:
        //             return slot is WeaponSlots.Right or WeaponSlots.TwoHanded
        //                 or (WeaponSlots.Right | WeaponSlots.TwoHanded);
        //         case WeaponType.Axe:
        //         case WeaponType.Bow:
        //         case WeaponType.Crossbow:
        //         case WeaponType.Staff:
        //         case WeaponType.Rifle:
        //             return slot is WeaponSlots.TwoHanded;
        //         case WeaponType.Dagger:
        //         case WeaponType.Item:
        //         case WeaponType.Pistol:
        //         case WeaponType.Mace:
        //             return slot is WeaponSlots.Left or WeaponSlots.Right or (WeaponSlots.Left | WeaponSlots.Right);
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, null);
        //     }
        // }

        public static Damage GetAttack(this IWeapon weapon)
        {
            weapon.Data.DataAsset.GetData(weapon.Data.VarResolver, out WeaponInstanceData values);
            return new Damage(values.damage,
                Random.value <= values.critChance,
                DamageType.PhysicalPierce,
                values.force,
                weapon.Owner);
        }
    }
}