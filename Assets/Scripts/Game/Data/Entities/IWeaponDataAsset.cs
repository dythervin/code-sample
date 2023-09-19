using Dythervin.ObjectPool.Component;

namespace Game.Data
{
    public interface IWeaponSkin
    {
    }

    public interface IWeaponDataAsset : IEquipmentDataAsset
    {
        float Range { get; }

        WeaponType WeaponType { get; }

        IComponentPoolOut<IWeaponSkin> SkinPool { get; }

        WeaponInstanceData GetData(IEntityROVars varResolver);
    }

    public static class WeaponDataExt
    {
        public static void GetData(this IWeaponDataAsset entityROVars, IEntityROVars varResolver,
            out WeaponInstanceData data)
        {
            data = entityROVars.GetData(varResolver);
        }
    }
}