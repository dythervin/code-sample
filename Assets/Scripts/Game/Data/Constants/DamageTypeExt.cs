using System;

namespace Game.Data
{
    public static class DamageTypeExt
    {
        public const float DefaultMeleeRange = 1.28f;
        public const float DefaultRangedRange = 5f;

        public static bool IsPhysical(this DamageType value)
        {
            switch (value)
            {
                case DamageType.PhysicalSlash:
                case DamageType.PhysicalPierce:
                case DamageType.PhysicalStrike:
                    return true;
                case DamageType.Fire:
                case DamageType.Lightning:
                case DamageType.Poison:
                case DamageType.Pure:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public static bool IsMagic(this DamageType value)
        {
            switch (value)
            {
                case DamageType.Fire:
                case DamageType.Lightning:
                case DamageType.Poison:
                    return true;
                case DamageType.PhysicalSlash:
                case DamageType.PhysicalPierce:
                case DamageType.PhysicalStrike:
                case DamageType.Pure:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}