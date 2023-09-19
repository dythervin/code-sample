using Dythervin.Common;
using Dythervin.Game.Framework.View;
using Game.Items;
using Sirenix.OdinInspector;

namespace Game.View.Equipment
{
    public class EntityEquipmentView : EntityComponentView<EntityEquipmentComponent>
    {
        private CharacterSlotViewComponent _slotViewComponent;

        private IViewMap _viewMap;

        [Button]
        public void UnequipArmor()
        {
            Model.UnequipAll();
        }

        protected override void Init()
        {
            base.Init();
            _viewMap = Context.ViewMap;
            Model.OnEquipped += Model_OnEquipped;
            Model.OnUnequipped += Model_OnUnequipped;
            Owner.GetViewComponent(out _slotViewComponent);
        }

        private void Model_OnUnequipped(EquipmentSlot equipmentSlot, IEquipment equipment)
        {
            _viewMap[equipment].Transform.SetParent(null, true);
        }

        private void Model_OnEquipped(EquipmentSlot equipmentSlot, IEquipment equipment)
        {
            _viewMap[equipment].Transform.SetParent(_slotViewComponent.Slots[equipmentSlot], false);
        }
    }
}