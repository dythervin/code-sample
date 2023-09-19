namespace Game.Data
{
    public interface IWeaponData : IEquipmentData
    {
        WeaponInstanceData Current { get; }

        new IWeaponDataAsset DataAsset { get; }
    }
}