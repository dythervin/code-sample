using Dythervin.Game.Framework;
using Dythervin.Game.Framework.View;
using Game.Items;

namespace Game.View
{
    public abstract class ItemView<TObserver> : ModelView<TObserver, IViewContextExt, IViewComponentExt>
        where TObserver : class, IItem, IModel
    {
    }
}