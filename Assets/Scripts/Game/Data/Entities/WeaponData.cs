using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Dythervin.AssetIdentifier;
using Dythervin.Common;

namespace Game.Data
{
    [Serializable]
    [Guid("ACD08CEF-50C8-4DDE-A2A7-61275B66D93D")]
    [Dythervin.Serialization.SourceGen.DSerializable]
    public class WeaponData<TDataAsset> : EquipmentData<TDataAsset>, IWeaponData
        where TDataAsset : class, IWeaponDataAsset
    {
        private WeaponInstanceData? _weaponInstanceData;

        public WeaponData(AssetId itemAssetId = default, Tag tags = default, int level = 0, bool isEquipped = false,
            IReadOnlyList<IEntityComponentDataExt> components = null) : base(itemAssetId,
            tags,
            level,
            isEquipped,
            components)
        {
        }

        public WeaponData()
        {
        }

        public WeaponInstanceData Current => _weaponInstanceData ??= DataAsset.GetData(VarResolver);

        IWeaponDataAsset IWeaponData.DataAsset => DataAsset;
    }

    [Guid("7838F92E-77BE-4162-8A3C-1281596D0DD6")]
    [Serializable]
    public class WeaponRangedData : WeaponData<IWeaponRangedDataAsset>, IWeaponRangedData
    {
    }

    public interface IWeaponRangedData : IWeaponData
    {
        new IWeaponRangedDataAsset DataAsset { get; }
    }
}