using System;
using System.Collections.Generic;
using Dythervin.Common;

namespace Game.Data
{
    public abstract class EquipmentInventoryComponentData : EntityComponentDataExt
    {
        private readonly EquipmentSlot[] _equippedSlots;

        public IReadOnlyList<EquipmentSlot> EquippedSlots => _equippedSlots;
        public override bool IsReadOnly => false;

        protected EquipmentInventoryComponentData(EquipmentSlot[] equippedSlots = null)
        {
            _equippedSlots = equippedSlots ?? Array.Empty<EquipmentSlot>();
        }
    }
}