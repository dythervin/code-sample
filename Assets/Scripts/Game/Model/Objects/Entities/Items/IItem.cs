using System;
using Dythervin.AssetIdentifier;
using Game.Data;

namespace Game.Items
{
    public interface IItem : IEntityExt
    {
        event Action OwnerChanged;

        IEntityExt Owner { get; }

        new IItemData Data { get; }

        AssetId ItemDataAssetId { get; }

        void SetOwner(IEntityExt owner);
    }
}