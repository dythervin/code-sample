namespace Game.Data
{
    public readonly struct WeaponInstanceData
    {
        public readonly float damage;

        public readonly float cooldown;

        public readonly float critChance;

        public readonly float critValue;

        public readonly float force;

        public readonly float range;

        public WeaponInstanceData(float damage, float cooldown, float critChance, float critValue, float force,
            float range)
        {
            this.damage = damage;
            this.cooldown = cooldown;
            this.critChance = critChance;
            this.critValue = critValue;
            this.force = force;
            this.range = range;
        }
    }
}