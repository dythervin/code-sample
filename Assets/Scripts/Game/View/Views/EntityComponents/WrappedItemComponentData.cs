using System.Runtime.InteropServices;
using Dythervin.AssetIdentifier;
using Game.Data;

namespace Game.View
{
    [System.Serializable]
    [Guid("AD65ED7C-F4E3-48BD-A683-103CA50504FC")]
    [Dythervin.Serialization.SourceGen.DSerializable]
    public sealed class WrappedItemComponentData : WrappedEntityData<IItemData>, IItemData
    {
        public AssetId DataAssetId => WrappedData.DataAssetId;

        public IItemDataAsset DataAsset => WrappedData.DataAsset;
    }
}