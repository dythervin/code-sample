using System.Collections.Generic;
using System.Runtime.InteropServices;
using Dythervin.AssetIdentifier;
using Dythervin.Common;

namespace Game.Data
{
    public interface IArmorData : IEquipmentData
    {
        new IArmorDataAsset DataAsset { get; }
    }

    [System.Serializable]
    [Dythervin.Serialization.SourceGen.DSerializable]
    [Guid("307D4783-7CC2-4811-A95A-CCE8A07F04D8")]
    public class ArmorData : EquipmentData<IArmorDataAsset>, IArmorData
    {
        public ArmorData(AssetId itemAssetId = default, Tag tags = default, int level = 0, bool isEquipped = false,
            IReadOnlyList<IEntityComponentDataExt> components = null) : base(itemAssetId,
            tags,
            level,
            isEquipped,
            components)
        {
        }

        IArmorDataAsset IArmorData.DataAsset => DataAsset;

        public ArmorData() : this(default)
        {
        }
    }
}