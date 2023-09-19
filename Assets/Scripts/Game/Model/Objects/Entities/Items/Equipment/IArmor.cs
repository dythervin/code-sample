using Game.Data;

namespace Game.Items
{
    public interface IArmor : IEquipment
    {
        new IArmorData Data { get; }
    }
}