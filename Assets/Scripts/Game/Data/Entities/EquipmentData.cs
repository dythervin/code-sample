using System.Collections.Generic;
using System.Runtime.InteropServices;
using Dythervin.AssetIdentifier;
using Dythervin.Common;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    [DSerializable]
    [Guid("8319C76F-D6BE-428A-839C-04E34A83A838")]
    public partial class EquipmentData<T> : ItemData<T>, IEquipmentData
        where T : class, IEquipmentDataAsset
    {
        [SerializeField]
        [Min(0)]
        [DSerializedField(20)]
        private int level;

        [SerializeField]
        [DSerializedField(21)]
        private bool isEquipped;

        public override bool IsReadOnly => false;

        protected EquipmentData(AssetId itemAssetId = default, Tag tags = default, int level = 0,
            bool isEquipped = false, IReadOnlyList<IEntityComponentDataExt> components = null) : base(itemAssetId)
        {
            Level = level;
            IsEquipped = isEquipped;
        }

        public int Level
        {
            get => level;
            set => level = value;
        }

        public bool IsEquipped
        {
            get => isEquipped;
            set => isEquipped = value;
        }

        IEquipmentDataAsset IEquipmentROData.DataAsset => DataAsset;
    }
}