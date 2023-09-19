using Dythervin.Game.Framework;
using Game.Data;
using Game.Game;

namespace Game.GameComponents
{
    public abstract class GameComponentExt<TData> : ModelComponent<IGameExt, IModelContextExt, TData>, IGameComponentExt
        where TData : class, IGameComponentDataExt
    {
        IGame IGameComponent.Owner => Owner;



        protected GameComponentExt(TData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }
    }
}