using System;
using System.Collections.Generic;
using Dythervin.Common;
using Dythervin.Game.Framework;
using Dythervin.Game.Framework.View;
using UnityEngine.Assertions;

namespace Game.View
{
    public class ViewComponentExt<TOwner> : DObject, IViewComponentInitializable
        where TOwner : class, IModelView
    {
        private InitState _lateInitState;

        private InitState _initState;

        public virtual bool AllowMultiple => false;

        IModelView IViewComponent.Owner => Owner;

        public IReadOnlyList<Type> FastTypeAccess => Array.Empty<Type>();

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

        public InitState InitState => _initState;

        protected virtual void Init()
        {
        }

        private void InitInternal()
        {
            _initState.AssertNotInitialized();

            _initState = InitState.Initializing;
            Init();
            _initState = InitState.Initialized;
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