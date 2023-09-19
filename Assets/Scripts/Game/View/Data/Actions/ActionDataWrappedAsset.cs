using Game.Data;

namespace Game.View.Data.Actions
{
    public abstract class ActionDataWrappedAsset<T> : ModelDataWrappedAsset<T>
        where T : class, IGAgentActionData
    {
        protected const string MenuName = "GAgentActions/";
    }
}