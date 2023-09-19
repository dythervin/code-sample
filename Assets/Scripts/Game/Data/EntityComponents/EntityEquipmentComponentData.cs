using Dythervin.Common;

namespace Game.Data
{
    [System.Runtime.InteropServices.Guid("765BCC6C-BB5A-4235-AF01-D968688165F3")]
    [Dythervin.Serialization.SourceGen.DSerializable]
    public class EntityEquipmentComponentData : EquipmentInventoryComponentData
    {
        public EntityEquipmentComponentData(EquipmentSlot[] equippedSlots = null) : base(equippedSlots)
        {
        }

        public EntityEquipmentComponentData() : base()
        {
        }
    }
}