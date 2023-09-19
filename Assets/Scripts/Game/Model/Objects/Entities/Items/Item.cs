using System;
using Dythervin.AssetIdentifier;
using Dythervin.Game.Framework;
using Game.Data;

namespace Game.Items
{
    public abstract class Item<TData> : EntityExt<TData>, IItem
        where TData : class, IItemData
    {
        public event Action OwnerChanged;

        public IEntityExt Owner { get; private set; }

        IItemData IItem.Data => Data;

        public AssetId ItemDataAssetId { get; }

        protected Item(TData data, IModelContextExt context, IModelConstructorContext constructorContext) : base(
            data,
            context,
            constructorContext)
        {
            ItemDataAssetId = data.DataAssetId;
        }

        public void SetOwner(IEntityExt owner)
        {
            if (Owner == owner)
                return;

            Owner = owner;
            OwnerChanged?.Invoke();
        }
    }
}