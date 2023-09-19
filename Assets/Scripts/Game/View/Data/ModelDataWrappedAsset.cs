using Dythervin.AssetIdentifier;
using Dythervin.Game.Framework.Data;
using Game.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.View.Data
{
    public abstract class ModelDataWrappedAsset : AssetIdentified
    {
    }

    public abstract class ModelDataWrappedAsset<T> : ModelDataWrappedAsset, IModelDataAssetWrapper<T>
        where T : class, IModelData
    {
        [HideLabel]
        [SerializeField] private T value;

        public T WrappedData => value;

        IModelData IModelDataWrapper.WrappedData => WrappedData;

        T IModelDataWrapper<T>.WrappedData => value;
    }
}