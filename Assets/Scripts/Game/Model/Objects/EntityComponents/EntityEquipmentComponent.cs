using Dythervin.Game.Framework;
using Game.Data;
using Game.Inventory;
using Game.Items;

namespace Game
{
    public class EntityEquipmentComponent : EntityEquipmentComponentBase<EntityEquipmentComponentData, IEquipment>,
        IEntityEquipment
    {
        public EntityEquipmentComponent(EntityEquipmentComponentData inventoryComponentData,
            IModelContextExt context, IModelConstructorContext constructorContext) : base(inventoryComponentData,
            context, constructorContext)
        {
        }
    }
}