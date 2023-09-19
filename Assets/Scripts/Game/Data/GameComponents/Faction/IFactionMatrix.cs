using Dythervin.Collections;

namespace Game.Data
{
    public interface IFactionMatrix : ICollisionMatrix<int>, IReadOnlyFactionMatrix
    {
        new int this[Faction a, Faction b] { get; set; }
    }
}