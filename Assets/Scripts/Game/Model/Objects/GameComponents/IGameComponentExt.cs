using Dythervin.Game.Framework;
using Game.Game;

namespace Game.GameComponents
{
    public interface IGameComponentExt : IGameComponent
    {
        new IGameExt Owner { get; }

        void SetOwner(IGameExt owner);
    }
}