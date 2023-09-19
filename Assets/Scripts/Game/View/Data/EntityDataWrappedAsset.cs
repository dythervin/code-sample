using Game.Data;

namespace Game.View.Data
{
    public class EntityDataWrappedAsset<T> : ModelDataWrappedAsset<T>
        where T : class, IEntityDataExt
    {
        protected const string MenuName = "Entities/";
    }
}