using Game.Data;

namespace Game.Common
{
    public readonly struct AttackOverTime
    {
        public readonly Damage damage;
        public readonly int ticks;
        public readonly float interval;
        public readonly DamageType damageType;

        public AttackOverTime(in Damage damage, int ticks, float interval, DamageType damageType)
        {
            this.damage = damage;
            this.ticks = ticks;
            this.interval = interval;
            this.damageType = damageType;
        }
    }
}