using System.Collections.Generic;
using Dythervin.Common;
using Dythervin.Game.Framework;
using Dythervin.Game.Framework.View;
using Dythervin.ObjectPool;
using Game.Data;
using Game.View.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.View
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ViewContext))]
    public class EntityViewExt<TEntityExt> : EntityView<TEntityExt, IViewContextExt, IViewComponentExt>,
        IEntityViewExt,
        IZenInstaller
        where TEntityExt : class, IEntityExt
    {
        [SerializeField] private Transform center;

        [SerializeField] private bool registerColliders;

        [Inject] private IEntityColliderMap _colliderMap;

        [Inject] private DiContainer _diContainer;

        private IHealthComponent _health;

        [CanBeNull] private List<int> _colliderCollection;

        IEntityExt IProvider<IEntityExt>.Data => Model;

        IEntityExt IEntityViewExt.Model => Model;

        IEntityExt IModelView<IEntityExt>.Model => Model;

        protected override void Init()
        {
            base.Init();
            if (registerColliders)
                CollectColliders();
        }

        protected override void Destroyed()
        {
            if (registerColliders)
                UnregisterColliders();

            base.Destroyed();
        }

        private void UnregisterColliders()
        {
            foreach (int colliderId in _colliderCollection)
            {
                _colliderMap.Remove(colliderId);
            }

            _colliderCollection.Clear();
        }

        protected override void ComponentViewAdded(IModelComponentView view)
        {
            base.ComponentViewAdded(view);
            _diContainer.Inject(view);
        }

        private void CollectColliders()
        {
            using var handler = ListPools<Collider>.Shared.Get(out var colliderBuffer);
            GetComponentsInChildren(colliderBuffer);
            _colliderCollection = new List<int>(colliderBuffer.Count);

            foreach (Collider collider in colliderBuffer)
            {
                int id = collider.GetInstanceID();
                _colliderMap.Add(id, Model);
                _colliderCollection.Add(id);
            }
        }

        public virtual void InstallBindings(DiContainer container)
        {
            container.Bind<IEntityExt>().FromInstance(Model).AsSingle();
            container.Bind<IEntity>().FromInstance(Model).AsSingle();
            container.Bind<IEntityViewExt>().FromInstance(this).AsSingle();
            container.Bind<IEntityView>().FromInstance(this).AsSingle();
            container.BindInstance(transform).AsCached();
            container.BindInstance(transform).WithId(InjectId.TransformRoot).AsCached();
            container.BindInstance(center).WithId(InjectId.TransformCenter).AsCached();
        }
    }
}