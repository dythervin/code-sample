using Dythervin.Game.Framework.View;

namespace Game.View
{
    public interface IEntityViewExt : IEntityView, IModelView<IEntityExt>
    {
        new IEntityExt Model { get; }
    }
}