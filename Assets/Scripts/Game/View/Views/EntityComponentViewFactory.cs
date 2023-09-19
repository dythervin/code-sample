using Dythervin.Game.Framework.View;

namespace Game.View
{
    public class EntityComponentViewFactory : ComponentViewFactory<IEntityComponentExt, IEntityComponentViewExt>
    {
        public EntityComponentViewFactory(IGameObjectFactory gameObjectFactory, IViewMap viewMap,
            IViewContext viewContext) : base(gameObjectFactory, viewMap, viewContext) { }
    }
}