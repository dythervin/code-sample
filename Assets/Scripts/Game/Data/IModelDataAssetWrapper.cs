using Dythervin.Game.Framework.Data;

namespace Game.Data
{
    public interface IModelDataAssetWrapper<out T> : IModelDataWrapper<T>
        where T : class, IModelData
    {
    }
}