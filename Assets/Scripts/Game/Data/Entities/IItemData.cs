using System.Runtime.InteropServices;
using Dythervin.AssetIdentifier;

namespace Game.Data
{
    [Guid("CC6A8E72-7E42-4E0B-84D2-FCB4FDE2C2C4")]
    public interface IItemData : IEntityDataExt, IItemROData
    {
    }

    public interface IItemROData : IEntityRODataExt
    {
        AssetId DataAssetId { get; }

        IItemDataAsset DataAsset { get; }
    }
}