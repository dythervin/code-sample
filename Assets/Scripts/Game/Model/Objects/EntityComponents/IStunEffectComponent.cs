namespace Game
{
    public interface IStunEffectComponent : IEntityComponentExt
    {
        bool IsStunned { get; }
    }
}