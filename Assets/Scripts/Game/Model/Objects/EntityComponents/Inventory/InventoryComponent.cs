using System.Collections.Generic;
using Dythervin.AssetIdentifier;
using Dythervin.Collections;
using Dythervin.Core;
using Dythervin.Game.Framework;
using Dythervin.ObjectPool;
using Game.Data;
using Game.Items;
using Game.Items.Inventory;
using Sirenix.OdinInspector;
using UnityEngine.Assertions;

namespace Game.Inventory
{
    public class InventoryComponent : EntityComponentExt<InventoryData>, IInventory
    {
        public event IInventory.ItemChangedHandler OnAdded;

        public event IInventory.ItemChangedHandler OnRemoved;

        public event IItemStackable.ItemAmountChangedHandler OnStackableChanged;

        private readonly IItemStackable.ItemAmountChangedHandler _onItemStackableAmountChangedCached;

        private readonly HashSet<IItem> _items = new();

        private readonly Dictionary<AssetId, IItemStackable> _stackableItems = new();

        public InventoryComponent(InventoryData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
            _onItemStackableAmountChangedCached = OnItemStackableAmountChanged;
        }

        [Button]
        [HideInEditorMode]
        public void Add(IItemStackable value)
        {
            if (!CanAdd(value))
                return;

            int change = value.Amount;
            if (_stackableItems.TryGetValue(value.ItemDataAssetId, out IItemStackable item))
            {
                item.Amount += change;
                value.Dispose();
                value = item;
            }
            else
            {
                _stackableItems.Add(value.ItemDataAssetId, value);
                value.ItemAmountChanged += _onItemStackableAmountChangedCached;
            }

            MarkDataDirty();
            OnStackableChanged?.Invoke(value, change);
        }

        private bool CanAdd(IItem value)
        {
            return !value.IsDisposed && !_items.Contains(value);
        }

        protected override void Constructed()
        {
            base.Constructed();
            foreach (IItemData itemData in Data.Items.ToEnumerable())
            {
                IItem item = Context.AnyFactory.Construct<IItem>(itemData);
                item.IsActive = false;
                Add(item);
            }
        }

        protected override void Init()
        {
            base.Init();
            foreach (IItem item in _items)
            {
                item.SetOwner(Owner);
            }
        }

        public override void Dispose()
        {
            foreach (IItem item in _items)
            {
                item.OnDataChanged -= MarkDataDirty;
                item.OnObjectDestroyed -= ValueOnOnObjectDestroyed;
                if (item is IItemStackable itemStackable)
                {
                    itemStackable.ItemAmountChanged -= _onItemStackableAmountChangedCached;
                }

                item.TryDispose();
            }

            base.Dispose();
        }

        private void OnItemStackableAmountChanged(IItemStackable item, int change)
        {
            OnStackableChanged?.Invoke(item, change);
        }

        private bool RemoveStackable(IItemStackable value)
        {
            if (!_stackableItems.TryGetValue(value.ItemDataAssetId, out IItemStackable itemStackable) ||
                value != itemStackable)
            {
                return false;
            }

            value.ItemAmountChanged -= _onItemStackableAmountChangedCached;

            MarkDataDirty();
            OnStackableChanged?.Invoke(itemStackable, itemStackable.Amount);
            return true;
        }

        [Button]
        [HideInEditorMode]
        public void Add(IItem value)
        {
            if (!CanAdd(value))
            {
                return;
            }

            Assert.IsNotNull(value);
            if (value is IItemStackable stackable)
            {
                Add(stackable);
                return;
            }

            _items.Add(value);
            MarkDataDirty();
            value.OnDataChanged += MarkDataDirty;
            value.OnObjectDestroyed += ValueOnOnObjectDestroyed;
            OnAdded?.Invoke(value);
        }

        private void ValueOnOnObjectDestroyed(IObject obj)
        {
            obj.OnObjectDestroyed -= ValueOnOnObjectDestroyed;
            Remove((IItem)obj);
        }

        [Button]
        [HideInEditorMode]
        public bool Remove(IItem value)
        {
            if (value is IItemStackable itemStackable && !RemoveStackable(itemStackable))
            {
                return false;
            }

            _items.Remove(value);
            MarkDataDirty();
            value.OnDataChanged -= MarkDataDirty;
            value.OnObjectDestroyed -= ValueOnOnObjectDestroyed;
            OnRemoved?.Invoke(value);
            return true;
        }

#if UNITY_EDITOR
        public void ClearEditor()
        {
            using var temp = _items.ToTempArray();
            _items.Clear();

            foreach (IItem itemRuntime in temp)
            {
                OnRemoved?.Invoke(itemRuntime);
            }
        }
#endif

        public HashSet<IItem>.Enumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}