namespace Game
{
    public interface IStatusEffectComponent : IEntityComponentExt
    {
        bool CanAct { get; }

        bool CanMove { get; }
    }
}