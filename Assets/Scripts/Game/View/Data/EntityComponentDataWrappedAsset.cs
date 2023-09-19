using Dythervin.Game.Framework.Data;

namespace Game.View.Data
{
    public class EntityComponentDataWrappedAsset<T> : ModelDataWrappedAsset<T>
        where T : class, IModelData
    {
        protected const string MenuName = "EntityComponents/";
    }
}