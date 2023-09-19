using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Data;

namespace Game
{
    public class EntityExtFactory<TEntity, TEntityData> : ModelFactory<TEntity, TEntityData>
        where TEntity : class, IEntityExt, IModelInitializable
        where TEntityData : class, IEntityROData
    {
        public EntityExtFactory(IModelContext context) : base(context)
        {
        }
    }
}