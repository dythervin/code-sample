using Dythervin.Collections;
using Dythervin.Serialization.SourceGen;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Data
{
    public class FactionRelationsGameComponentData : GameComponentDataExt
    {
        [DSerializedField(10)]
        [SerializeField] private FactionMatrix matrix;

        public IReadOnlyFactionMatrix Matrix => matrix;

        public FactionRelationsGameComponentData(FactionMatrix matrix)
        {
            this.matrix = matrix;
        }

        public override bool IsReadOnly => false;
        public FactionRelationsGameComponentData()
        {
        }

        [Button]
        public int Get(Faction a, Faction b)
        {
            return matrix[a, b];
        }

        [Button]
        private void SetAll(Faction a, int value)
        {
            foreach (Faction b in FactionMatrix.FactionValues.ToEnumerable())
            {
                if (a == b || b == Faction.None)
                    continue;

                matrix[a, b] = value;
            }
        }
    }
}