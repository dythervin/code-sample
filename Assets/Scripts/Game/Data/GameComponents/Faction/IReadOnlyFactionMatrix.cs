using Dythervin.Collections;

namespace Game.Data
{
    public interface IReadOnlyFactionMatrix : IReadOnlyCollisionMatrix<int>
    {
        int this[Faction a, Faction b] { get; }
    }
}