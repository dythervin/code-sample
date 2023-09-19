using System;
using Dythervin.Common;
using Game.Data;

namespace Game.Items.Inventory
{
    public interface IReadOnlyCharacterEquipment
    {
        int EquippedSlotCount { get; }

        bool CanEquip(EquipmentSlot slot);

        bool CanEquip(IEquipmentROData equipmentData);
    }

    public interface IReadOnlyCharacterEquipment<out TEquipment> : IReadOnlyCharacterEquipment
        where TEquipment : class, IEquipment
    {
        event Action<EquipmentSlot, TEquipment> OnEquipped;

        event Action<EquipmentSlot, TEquipment> OnUnequipped;

        bool IsEquipped(EquipmentSlot slot);

        TEquipment this[EquipmentSlot slot] { get; }
    }
}