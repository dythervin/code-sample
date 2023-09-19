using Dythervin.Game.Framework;
using Game.Data;

namespace Game.GameComponents.Faction
{
    public class FactionRelationsGameComponent : GameComponentExt<FactionRelationsGameComponentData>,
        IFactionRelationsGameComponent
    {
        private readonly FactionMatrix _matrix;

        public IFactionMatrix FactionMatrix => _matrix;

        public FactionRelationsGameComponent(FactionRelationsGameComponentData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
            _matrix = new FactionMatrix(data.Matrix);
        }

        public bool IsAlly(Data.Faction a, Data.Faction b)
        {
            return a == b || _matrix[a, b] >= 100;
        }

        public bool IsHostile(Data.Faction a, Data.Faction b)
        {
            return a != b && _matrix[a, b] <= -100;
        }
    }
}