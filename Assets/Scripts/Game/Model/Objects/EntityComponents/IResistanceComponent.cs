using Game.Data;

namespace Game
{
    public interface IResistanceComponent : IEntityComponentExt
    {
        float this[DamageType damageType] { get; }

        void Calculate(DamageType damageType, ref float damage);
    }

    public static class ResistanceComponentExtensions
    {
        public static void Calculate(this IResistanceComponent resistanceComponent, ref Common.Damage damage)
        {
            float amount = damage.amount;
            resistanceComponent.Calculate(damage.type, ref amount);
            damage = new Common.Damage(damage, amount);
        }
    }
}