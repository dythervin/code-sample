using Dythervin.Game.Framework.View;

namespace Game.View
{
    public interface IEntityComponentViewExt : IEntityComponentView, IModelView
    {
        new IEntityViewExt Owner { get; }
    }
}