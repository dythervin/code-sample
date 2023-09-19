using System;
using System.Collections.Generic;
using Dythervin.Core.Lockables;
using Dythervin.Game.Framework;
using Game.Data;

namespace Game
{
    public abstract class LockableComponent<T> : EntityComponentExt<T>, ILockableComponent
        where T : class, IEntityComponentDataExt
    {
        public event Action OnLockChanged;

        private readonly HashSet<object> _ownerLockers = new();

        private bool _isLocked;

        private int _lockedTokens;

        public bool IsLocked
        {
            get => _isLocked;
            private set
            {
                if (_isLocked == value)
                {
                    return;
                }

                _isLocked = value;
                OnLockChanged?.Invoke();
            }
        }

        public LockableComponent(T componentData, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(componentData, context, constructorContext)
        {
        }

        private void UpdateLock()
        {
            IsLocked = _lockedTokens > 0 || _ownerLockers.Count > 0;
        }

        public ILockToken CreateLocker()
        {
            return new LockToken(this);
        }

        public void AddLocker<T>(T owner)
            where T : class
        {
            _ownerLockers.Add(owner);
        }

        public void RemoveLocker<T>(T owner)
            where T : class
        {
            _ownerLockers.Remove(owner);
        }

        private class LockToken : ILockToken
        {
            public event Action OnLockChanged;

            private LockableComponent<T> _lockableComponent;

            public bool IsLocked { get; private set; }

            public LockToken(LockableComponent<T> lockableComponent)
            {
                _lockableComponent = lockableComponent ?? throw new NullReferenceException();
            }

            public ILockableSimple SetLock(bool isLocked)
            {
                if (_lockableComponent == null)
                    throw new ObjectDisposedException(nameof(LockToken));

                if (IsLocked == isLocked)
                    return this;

                IsLocked = isLocked;
                if (isLocked)
                {
                    _lockableComponent._lockedTokens++;
                }
                else
                {
                    _lockableComponent._lockedTokens--;
                }

                _lockableComponent.UpdateLock();
                OnLockChanged?.Invoke();
                return this;
            }

            public void Dispose()
            {
                SetLock(false);
                _lockableComponent = null;
            }
        }
    }
}