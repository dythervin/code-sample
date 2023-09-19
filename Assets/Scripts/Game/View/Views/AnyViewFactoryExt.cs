using Dythervin.Game.Framework.View;
using Zenject;

namespace Game.View
{
    public class AnyViewFactoryExt : AnyViewFactory
    {
        [Inject]
        public AnyViewFactoryExt(IGameObjectFactory gameObjectFactory, IViewMap viewMap) : base(gameObjectFactory, viewMap)
        { }
    }
}