using System;
using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Common;
using Dythervin.Core;
using Dythervin.Core.Extensions;
using Dythervin.Game.Framework;
using Dythervin.ObjectPool;
using Game.Data;
using Game.Items;
using Game.Items.Inventory;

namespace Game.Inventory
{
    public abstract class EntityEquipmentComponentBase<TComponentData, TEquipment> : EntityComponentExt<TComponentData>,
        ICharacterEquipment<TEquipment>
        where TComponentData : EquipmentInventoryComponentData
        where TEquipment : class, IEquipment
    {
        private IInventory _inventory;

        public event Action<EquipmentSlot, TEquipment> OnEquipped;

        public event Action<EquipmentSlot, TEquipment> OnUnequipped;

        private readonly List<Predicate<IEquipmentROData>> _validators = new List<Predicate<IEquipmentROData>>();

        private readonly DictionaryCross<EquipmentSlot, TEquipment> _slotEquipmentMap = new();

        private readonly Dictionary<IEquipment, EquipmentSlot> _equipmentSlotMap = new();

        public int EquippedSlotCount => _slotEquipmentMap.Count;

        public TEquipment this[EquipmentSlot slot] => _slotEquipmentMap[slot];

        protected EntityEquipmentComponentBase(TComponentData inventoryComponentData, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(inventoryComponentData, context, constructorContext)
        {
        }

        protected override void Init()
        {
            base.Init();
            Owner.GetComponent(out _inventory);
        }

        protected override void LateInit()
        {
            base.LateInit();
            TComponentData data = Data;
            if (data.EquippedSlots.Count > 0)
            {
                var slots = data.EquippedSlots;
                int i = 0;
                foreach (IItem itemData in _inventory)
                {
                    if (itemData is TEquipment equipmentData && equipmentData.IsEquipped)
                    {
                        TryEquip(slots[i++], equipmentData);
                        if (i >= slots.Count)
                            break;
                    }
                }
            }
        }

        public IDisposable AddValidator(Predicate<IEquipmentROData> func)
        {
            _validators.Add(func);
            return new Token(func, this);
        }

        public void RemoveValidator(Predicate<IEquipmentROData> func)
        {
            _validators.Remove(func);
        }

        public void ClearValidator()
        {
            _validators.Clear();
        }

        public bool TryEquip(EquipmentSlot slot, TEquipment equipment)
        {
            if (!CanEquip(slot) || !CanEquip(equipment))
                return false;

            Equip(slot, equipment);
            return true;
        }

        private void Equip(EquipmentSlot slot, TEquipment equipment)
        {
            _equipmentSlotMap.Add(equipment, slot);
            foreach (EquipmentSlot equipmentSlot in slot)
            {
                _slotEquipmentMap.Add(equipmentSlot, equipment);
            }

            equipment.OnObjectDestroyed += EquipmentOnOnObjectDestroyed;
            MarkDataDirty();
            Equipped(slot, equipment);
            OnEquipped?.Invoke(slot, equipment);
        }

        protected EquipmentSlot[] GetCurrentSlotsOrdered()
        {
            int count = EquippedSlotCount;
            if (count == 0)
                return Array.Empty<EquipmentSlot>();

            var slots = new EquipmentSlot[count];
            int i = 0;
            foreach (IItem itemData in _inventory)
            {
                if (itemData is TEquipment equipment && equipment.IsEquipped)
                {
                    slots[i++] = _equipmentSlotMap[equipment];
                    if (i >= count)
                        break;
                }
            }

            return slots;
        }

        public bool TryEquip(TEquipment equipment)
        {
            if (!CanEquip(equipment))
                return false;

            foreach (EquipmentSlot slot in equipment.Data.DataAsset.Slots.ToEnumerable())
            {
                if (TryEquip(slot, equipment))
                    return true;
            }

            return false;
        }

        private void EquipmentOnOnObjectDestroyed(IObject obj)
        {
            if (!ApplicationExt.IsQuitting)
                TryUnequip((TEquipment)obj);
        }

        public bool TryUnequip(TEquipment equipment)
        {
            if (!_equipmentSlotMap.Remove(equipment, out EquipmentSlot slots))
                return false;

            if (!_slotEquipmentMap.Remove(slots, equipment))
                throw new Exception("Not equipped");

            foreach (EquipmentSlot slot in slots)
            {
                if (!_slotEquipmentMap.Remove(slot, equipment))
                    throw new Exception("Not equipped");
            }

            equipment.OnObjectDestroyed -= EquipmentOnOnObjectDestroyed;
            MarkDataDirty();
            Unequipped(slots, equipment);
            OnUnequipped?.Invoke(slots, equipment);
            return true;
        }

        public bool CanEquip(EquipmentSlot slot)
        {
            if (slot.Count == 0)
                return false;

            if (IsEquipped(slot))
                return false;

            foreach (EquipmentSlot equipmentSlot in slot)
            {
                if (IsEquipped(equipmentSlot))
                    return false;
            }

            return true;
        }

        public bool CanEquip(IEquipmentROData equipment)
        {
            for (int i = _validators.Count - 1; i >= 0; i--)
            {
                var validator = _validators[i];
                if (!validator.Invoke(equipment))
                    return false;
            }

            return true;
        }

        protected virtual void Unequipped(EquipmentSlot slot, TEquipment equipment)
        {
        }

        protected virtual void Equipped(EquipmentSlot slot, TEquipment equipment)
        {
        }

        public virtual bool IsEquipped(EquipmentSlot slot)
        {
            AssertSingleSlot(slot);
            return _slotEquipmentMap.ContainsKey(slot);
        }

        private static void AssertSingleSlot(EquipmentSlot slot)
        {
            if (slot.Count != 1)
                throw new Exception();
        }

        public bool TryGetEquipped(EquipmentSlot slot, out TEquipment equipment)
        {
            return _slotEquipmentMap.TryGetValue(slot, out equipment);
        }

        public bool TryGetEquipped(TEquipment equipment, out EquipmentSlot slot)
        {
            return _equipmentSlotMap.TryGetValue(equipment, out slot);
        }

        public virtual bool TryUnequip(EquipmentSlot slot)
        {
            return _slotEquipmentMap.TryGetValue(slot, out TEquipment equipment) && TryUnequip(equipment);
        }

        public void UnequipAll()
        {
            if (EquippedSlotCount == 0)
            {
                return;
            }

            using var temp = new PooledArrayHandler<TEquipment>(EquippedSlotCount);
            int i = 0;
            foreach (TEquipment value in _slotEquipmentMap.Values)
            {
                temp[i++] = value;
            }

            foreach (TEquipment equipmentSlot in temp)
            {
                TryUnequip(equipmentSlot);
            }
        }

        public bool CanEquip(TEquipment equipment)
        {
            return CanEquip(equipment.Data);
        }

        private class Token : IDisposable
        {
            private Predicate<IEquipmentROData> _predicate;

            private EntityEquipmentComponentBase<TComponentData, TEquipment> _equipmentComponentBase;

            public Token(Predicate<IEquipmentROData> predicate,
                EntityEquipmentComponentBase<TComponentData, TEquipment> equipmentComponentBase)
            {
                _predicate = predicate;
                _equipmentComponentBase = equipmentComponentBase;
            }

            void IDisposable.Dispose()
            {
                if (_predicate == null || _equipmentComponentBase == null)
                    return;

                _equipmentComponentBase._validators.Remove(_predicate);
                _equipmentComponentBase = null;
                _predicate = null;
            }
        }
    }
}