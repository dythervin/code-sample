namespace Game.Data
{
    public interface IWeaponRangedDataAsset : IWeaponDataAsset
    {
        bool IsRaycast { get; }
    }
}