using System.Runtime.InteropServices;
using Dythervin.Core.Extensions;
using Dythervin.Serialization.SourceGen;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    [DSerializable]
    [Guid("64512D81-232D-4F29-AD1D-4DAF47F7ACEB")]
    public partial class FactionComponentData : EntityComponentDataExt
    {
        [SerializeField]
        [DSerializedField(10)]
        private bool random;

        [HideIf(nameof(random))] [SerializeField]
        [DSerializedField(11)]
        private Faction faction;

        public override bool IsReadOnly => false;

        public Faction Faction
        {
            get
            {
                if (random)
                {
                    faction = faction.GetValues(true).GetRandom(1);
                    random = false;
                }

                return faction;
            }
            set
            {
                faction = value;
                random = false;
            }
        }

        public FactionComponentData()
        {
            random = true;
        }

        public FactionComponentData(Faction faction)
        {
            this.faction = faction;
        }
    }
}