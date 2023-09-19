using Dythervin.Game.Framework.View;
using Zenject;

namespace Game.View
{
    public class ViewContextExt : IViewContextExt
    {
        public IGameObjectFactory GameObjectFactory { get; }

        public IViewMap ViewMap { get; }

        [Inject]
        public ViewContextExt(IGameObjectFactory gameObjectFactory, IViewMap viewMap)
        {
            GameObjectFactory = gameObjectFactory;
            ViewMap = viewMap;
        }
    }
}