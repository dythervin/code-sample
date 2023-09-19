using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [System.Runtime.InteropServices.Guid("463F19F0-968A-4226-81FF-8806F97D8AE1")]
    [DSerializable]
    [System.Serializable]
    public partial class InventoryData : EntityComponentDataExt
    {
        [SerializeField]
        [DSerializedField(10)]
        // ReSharper disable once InconsistentNaming
        private SerializableHashList<IItemData> items;

        public IReadOnlyList<IItemData> Items => items.hashList;

        public InventoryData(IReadOnlyList<IItemData> items)
        {
            this.items = new SerializableHashList<IItemData>(items != null ? new HashList<IItemData>(items) : new HashList<IItemData>());
        }

        public override bool IsReadOnly => false;

        public InventoryData()
        {
        }
    }
}