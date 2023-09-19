using Dythervin.Game.Framework;

namespace Game.View
{
    public interface IEntityComponentView<out TObserver> : Dythervin.Game.Framework.View.IEntityComponentView<TObserver>
        where TObserver : class, IEntityComponent
    {
        new IEntityViewExt Owner { get; }
    }
}