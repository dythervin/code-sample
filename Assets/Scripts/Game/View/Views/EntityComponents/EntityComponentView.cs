using Dythervin.Game.Framework;
using Dythervin.Game.Framework.View;

namespace Game.View
{
    public interface IEntityComponentViewExt<out T> : IEntityComponentView<T>
        where T : class, IEntityComponentExt
    {
        new IEntityViewExt Owner { get; }
    }


    public abstract class EntityComponentView<TObserver>
        : ModelComponentView<IEntityViewExt, TObserver, IViewContextExt, IViewComponentExt>, IEntityComponentViewExt,
            IEntityComponentView<TObserver>
        where TObserver : class, IEntityComponentExt, IModel
    {
        IEntityComponent IEntityComponentView.Model => Model;

        IEntityView IEntityComponentView.Owner => Owner;

        IEntityViewExt IEntityComponentViewExt.Owner => Owner;

        IModelView IModelComponentView.Owner => Owner;
    }
}