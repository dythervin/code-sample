using System;
using System.Collections.Generic;
using Dythervin.Common;
using Dythervin.Game.Framework;
using Dythervin.Game.Framework.View;
using UnityEngine.Assertions;

namespace Game.View
{
    public class MonoViewComponentExt<TOwner> : MonoView, IViewComponentInitializable
        where TOwner : class, IModelView
    {
        public event Action OnOwnerChanged;

        private InitState _lateInitState;

        public virtual bool AllowMultiple => false;

        IModelView IViewComponent.Owner => Owner;

        IReadOnlyList<Type> IComponent.FastTypeAccess => Array.Empty<Type>();

        public TOwner Owner { get; private set; }

        InitState ILateInitializable.LateInitState => _lateInitState;

        IObject IComponent.Owner => Owner;

        public virtual void SetOwner(TOwner owner)
        {
            Assert.IsTrue(owner != Owner);
            Owner?.ViewComponents?.Remove(this);
            Owner = owner;
            Owner?.ViewComponents?.Add(this);
            OwnerChanged();
            OnOwnerChanged?.Invoke();
        }

        protected virtual void OwnerChanged()
        {
        }

        void ILateInitializable.LateInit()
        {
            _lateInitState.AssertNotInitialized();

            _lateInitState = InitState.Initializing;
            LateInit();
            _lateInitState = InitState.Initialized;
        }

        void IComponent.SetOwner(IObject owner)
        {
            SetOwner((TOwner)owner);
        }

        public InitState InitState { get; private set; }

        protected virtual void Init()
        {
        }

        private void InitInternal()
        {
            InitState.AssertNotInitialized();
            InitState = InitState.Initializing;
            Init();
            InitState = InitState.Initialized;
        }

        protected virtual void LateInit()
        {
        }

        void IInitializable.Init()
        {
            InitInternal();
        }
    }
}