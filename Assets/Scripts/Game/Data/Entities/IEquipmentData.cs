namespace Game.Data
{
    public interface IEquipmentData : IItemData, IEquipmentROData
    {
        new int Level { get; set; }

        new bool IsEquipped { get; set; }
    }

    public interface IEquipmentROData : IItemROData
    {
        int Level { get; }

        bool IsEquipped { get; }

        new IEquipmentDataAsset DataAsset { get; }
    }
}