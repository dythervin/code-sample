using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Common;

namespace Game.Data
{
    public static class EquipmentSlotsExtensions
    {
        public static bool IsWeaponSlot(this EquipmentSlot slot)
        {
            return slot == EquipmentSlots.MainHand || slot == (EquipmentSlots.OffHand | EquipmentSlots.MainHand);
        }

        public static bool ContainsInItems(this IReadOnlyList<EquipmentSlot> slots, EquipmentSlot slot)
        {
            foreach (EquipmentSlot x in slots.ToEnumerable())
            {
                if (x.Contains(slot))
                    return true;
            }

            return false;
        }
    }
}