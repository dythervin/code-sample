using System;
using Dythervin.Common;
using Dythervin.Common.ID;
using Dythervin.Game.Framework;
using Game.Data;
using UnityEngine;
using Zenject;

namespace Game
{
    public abstract class EntityExt<TData> : Model<TData, IModelContextExt, IEntityComponentExt>, IEntityExt
        where TData : class, IEntityDataExt
    {
        [Inject] private Transform _transform;

        [Inject(Id = InjectId.TransformCenter)]
        private Transform _center;

        private DiContainer _diContainer;

        private bool _isActive = true;

        public float Radius => Data.Radius;

        public event Action OnActiveChanged;

        public event Action<IEntity> OnObjectActiveChanged;

        public Tag Tags
        {
            get => Data.Tags;
            set
            {
                if (Data.Tags == value)
                    return;

                Data.Tags = value;
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive == value)
                    return;

                _isActive = value;
                OnObjectActiveChanged?.Invoke(this);
                OnActiveChanged?.Invoke();
            }
        }

        public Vector3 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        public Transform Transform => _transform;

        IEntityDataExt IEntityExt.Data => Data;

        public Vector3 Center => _center.position;

        IReadOnlyObjectListOut<IEntityComponent> IEntity.Components => Components;

        protected EntityExt(TData data, IModelContextExt context, IModelConstructorContext constructorContext) :
            base(data, context, constructorContext)
        {
        }

        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }

        protected override void ComponentAdded(IEntityComponentExt component)
        {
            _diContainer.Inject(component);
            base.ComponentAdded(component);
        }

        [Inject]
        private void InitDiContainer(DiContainer diContainer)
        {
            _diContainer = diContainer;
            InstallBindings(diContainer);
            Inject(diContainer);
        }

        protected virtual void InstallBindings(DiContainer diContainer)
        {
            diContainer.BindInterfacesAndSelfTo<EntityVars>().FromInstance(Data.VarResolver).AsSingle();
        }

        protected override void ComponentsConstructed()
        {
            base.ComponentsConstructed();

            foreach (IEntityComponentExt component in components)
            {
                if (!component.AllowMultiple)
                    _diContainer.BindInterfacesTo(component.GetType()).FromInstance(component).AsSingle();
            }

            foreach (IEntityComponentExt component in components)
            {
                _diContainer.Inject(component);
            }
        }

        protected virtual void Inject(DiContainer diContainer)
        {
        }

        protected override void Destroyed()
        {
            IsActive = false;
            base.Destroyed();
        }

        protected IEntityComponentDataExt[] GetNewComponentsDataArray()
        {
            return GetNewComponentsDataArray<IEntityComponentDataExt>();
        }
    }
}