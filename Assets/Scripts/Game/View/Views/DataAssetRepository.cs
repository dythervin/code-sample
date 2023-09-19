using System;
using System.Collections.Generic;
using Dythervin.Core.Extensions;
using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;
using UnityEngine.Assertions;

namespace Game.View
{
    public class DataAssetRepository : IDataAssetRepository
    {
        private readonly IAssetDatabaseService _assetDatabaseService;
        private readonly Dictionary<string, IDataAsset> _cachedDataAsset = new Dictionary<string, IDataAsset>();

        public DataAssetRepository(IAssetDatabaseService assetDatabaseService)
        {
            _assetDatabaseService = assetDatabaseService;
        }

        public IDataAsset Get(string key)
        {
            if (_cachedDataAsset.TryGetValue(key, out IDataAsset dataAsset))
                return dataAsset;

            return null;
        }

        public bool TryLoadIAsset<TDataAsset>(out TDataAsset dataAsset)
            where TDataAsset : class, IDataAsset
        {
            Type type = typeof(TDataAsset);
            Assert.IsTrue(type.IsInterface);

            string key = DataAssetHelper.GetIAddressablesKey(type);
            return TryLoadIAsset(key, out dataAsset);
        }

        public bool TryLoadIAsset<TDataAsset>(string key, out TDataAsset dataAsset)
            where TDataAsset : class, IDataAsset
        {
            return TryLoadAssetInner(key, out dataAsset);
        }

        public bool TryLoadAsset<TDataAsset>(out TDataAsset dataAsset)
            where TDataAsset : class, IDataAsset, new()
        {
            Type type = typeof(TDataAsset);
            Assert.IsTrue(type.IsClass && type.IsInstantiatable());

            string key = DataAssetHelper.GetAddressablesKey(type);
            return TryLoadAsset(key, out dataAsset);
        }

        public bool TryLoadAsset<TDataAsset>(string key, out TDataAsset dataAsset)
            where TDataAsset : class, IDataAsset, new()
        {
            return TryLoadAssetInner(key, out dataAsset);
        }

        public bool TryLoadAssetInner<TDataAsset>(string key, out TDataAsset dataAsset)
            where TDataAsset : class, IDataAsset
        {
            if (_cachedDataAsset.TryGetValue(key, out IDataAsset iDataAsset))
            {
                dataAsset = (TDataAsset)iDataAsset;
                return true;
            }

            if (_assetDatabaseService.TryGetAsset(new AssetKey(key, typeof(TDataAsset).GetHashCode()),
                    out dataAsset))
            {
                _cachedDataAsset.Add(key, dataAsset);
                return true;
            }

            return false;
        }
    }
}