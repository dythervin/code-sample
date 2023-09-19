using Dythervin.Game.Framework;
using Game.GameComponents;

namespace Game.Game
{
    public interface IGameExt : IGame
    {
        new IReadOnlyObjectListOut<IGameComponentExt> Components { get; }

        new IReadOnlyObjectListOut<IEntityExt> Entities { get; }
        new IReadOnlyObjectListOut<IEntityExt> ActiveEntities { get; }
    }
}