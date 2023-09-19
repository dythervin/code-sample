using Dythervin.Game.Framework;
using Dythervin.Game.Framework.View;
using UnityEngine;
using Zenject;

namespace Game.View
{
    public class EntityViewFactory : ViewFactory<IEntityExt, IEntityViewExt>
    {
        private readonly DiContainer _container;

        private readonly Transform _defaultParent;

        public EntityViewFactory(IGameObjectFactory gameObjectFactory, IViewMap viewMap, IViewContext viewContext,
            DiContainer container, Transform defaultParent) : base(gameObjectFactory, viewMap, viewContext)
        {
            _container = container;
            _defaultParent = defaultParent;
        }

        public override bool TryConstruct(IEntityExt observer, out IEntityViewExt view, Transform parent = null)
        {
            // ReSharper disable once Unity.NoNullCoalescing
            return base.TryConstruct(observer, out view, parent ?? _defaultParent);
        }

        protected override void Constructed(IModel model, IEntityViewExt view)
        {
            base.Constructed(model, view);
            DiContainer subContainer = _container.CreateSubContainer();
            subContainer.QueueForInject(model);
            subContainer.Inject(view.GameObject.GetComponent<Context>());
        }
    }
}