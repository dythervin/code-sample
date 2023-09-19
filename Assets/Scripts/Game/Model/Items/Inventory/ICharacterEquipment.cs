using Dythervin.Common;

namespace Game.Items.Inventory
{
    public interface ICharacterEquipment<TEquipment> : IReadOnlyCharacterEquipment<TEquipment>
        where TEquipment : class, IEquipment
    {
        bool TryGetEquipped(EquipmentSlot slot, out TEquipment equipment);

        bool TryEquip(EquipmentSlot slot, TEquipment equipment);

        bool TryEquip(TEquipment equipment);

        bool TryUnequip(TEquipment weapon);

        bool TryUnequip(EquipmentSlot slot);

        void UnequipAll();

        bool CanEquip(TEquipment equipment);
    }
}