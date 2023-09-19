using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Data;

namespace Game
{
    public class EntityComponentFactory<TComponent, TObjectData> : ModelComponentFactory<TComponent, TObjectData>
        where TComponent : class, IEntityComponent, IModelInitializable
        where TObjectData : class, IEntityComponentData
    {

        public EntityComponentFactory(IModelContext context) : base(context) { }
    }
}