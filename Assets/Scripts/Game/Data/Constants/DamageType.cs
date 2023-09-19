namespace Game.Data
{
    public enum DamageType : byte
    {
        None = 0,
        PhysicalSlash = 1,
        PhysicalPierce = 2,
        PhysicalStrike = 3,

        Fire = 16,
        Lightning = 17,
        Poison = 18,

        Pure = byte.MaxValue
    }

    public enum DamageMethod : byte
    {
        Instant = 0,
        Delayed = 1,
        OverTime = 2
    }
}