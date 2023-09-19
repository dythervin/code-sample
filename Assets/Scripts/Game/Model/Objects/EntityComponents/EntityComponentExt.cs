using Dythervin.Game.Framework;
using Game.Data;

namespace Game
{
    public abstract class EntityComponentExt<TData> : ModelComponent<IEntityExt, IModelContextExt, TData>, IEntityComponentExt
        where TData : class, IEntityComponentDataExt
    {
        IEntity IEntityComponent.Owner => Owner;
        
        IObject IComponent.Owner => Owner;

        IModel IModelComponent.Owner => Owner;

        protected EntityComponentExt(TData componentDataInventoryComponentData, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(componentDataInventoryComponentData, context, constructorContext)
        {
        }

        void IEntityComponent.SetOwner(IEntity owner)
        {
            SetOwner((IEntityExt)owner);
        }
    }
}