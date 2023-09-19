using Dythervin.Game.Framework;
using Game.Data;
using UnityEngine.Assertions;

namespace Game.Items
{
    public abstract class ItemStackable<TItemStackableData, TDataAsset> : Item<TItemStackableData>,
        IItemStackable
        where TItemStackableData : class, IItemStackableData
        where TDataAsset : class, IItemDataAsset
    {
        public event IItemStackable.ItemAmountChangedHandler ItemAmountChanged;

        public int Amount
        {
            get => Data.Amount;
            set
            {
                if (Amount == value)
                    return;

                Assert.IsTrue(Amount > 0);
                int change = value - Amount;
                Data.Amount = value;
                MarkDataDirty();
                ItemAmountChanged?.Invoke(this, change);
                if (value == 0)
                    Dispose();
            }
        }

        public ItemStackable(TItemStackableData inventoryComponent, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(inventoryComponent, context, constructorContext)
        {
        }
    }

    public class ItemStackable : ItemStackable<IItemStackableData, IItemDataAsset>
    {
        public ItemStackable(IItemStackableData inventoryComponent, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(inventoryComponent, context, constructorContext)
        {
        }
    }
}