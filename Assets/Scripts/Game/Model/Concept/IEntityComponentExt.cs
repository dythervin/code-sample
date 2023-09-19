using Dythervin.Game.Framework;

namespace Game
{
    public interface IEntityComponentExt : IEntityComponent
    {
        new IEntityExt Owner { get; }

        void SetOwner(IEntityExt owner);
    }
}