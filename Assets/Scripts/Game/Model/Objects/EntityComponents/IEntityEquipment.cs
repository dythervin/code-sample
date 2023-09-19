using Game.Items;
using Game.Items.Inventory;

namespace Game
{
    public interface IEntityEquipment : ICharacterEquipment<IEquipment>, IEntityComponentExt
    {
    }
}