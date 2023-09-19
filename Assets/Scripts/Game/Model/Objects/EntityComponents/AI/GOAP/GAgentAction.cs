using System;
using System.Collections.Generic;
using Dythervin.AI.GOAP;
using Dythervin.Common;
using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Data;
using Dythervin.Routines;
using Game.AI.GOAP;
using Game.Data;

namespace Game.AI
{
    public abstract class GAgentAction<TData> : ModelComponent<IGAgentComponent, IModelContextExt, TData>, IGAgentAction
        where TData : class, IGAgentActionData
    {
        private IDelayedCall<GAgentAction<TData>> _delayedCall;

        public event Action<bool> OnCompleted;

        public event Action OnEntered;

        public event Action OnExited;

        protected TData InitialData { get; }

        public StateState State { get; private set; }

        public bool IsActive => State == StateState.Active;

        public float Cost => InitialData.BaseCost;

        public IReadOnlyList<StateValue> Conditions => InitialData.Conditions;

        public IReadOnlyList<StateValueResult> Result => InitialData.Result;

        public string ActionName => GetType().Name;

        public SuspiciousLevel SuspiciousLevel => InitialData.SuspiciousLevel;

        protected GPlanParameters Parameters => Owner.PlanParameters;

        IObject IComponent.Owner => Owner;

        IModel IModelComponent.Owner => Owner;

        protected GAgentAction(TData data, IModelContextExt context, IModelConstructorContext constructorContext) :
            base(data, context, constructorContext)
        {
            InitialData = data.EnsureNotWrapped();
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnExit()
        {
        }

        protected void Complete(bool success)
        {
            if (State != StateState.Active && State != StateState.Entering)
                throw new Exception("Can't complete, action is not active");

            _Complete(success);
        }

        protected bool TryComplete(bool success)
        {
            if (State != StateState.Active && State != StateState.Entering)
                return false;

            _Complete(success);
            return true;
        }

        protected void CompleteDelayed(bool success, float delay)
        {
            if (delay >= 0)
            {
                if (_delayedCall == null || _delayedCall.IsDisposed)
                {
                    _delayedCall = DelayedCaller.Add(this,
                        success,
                        delay,
                        (action, data) =>
                        {
                            action._delayedCall = null;
                            action.Complete(data);
                        });
                }
            }
            else
                Complete(success);
        }

        protected virtual void OnCompleting(bool success)
        {
        }

        protected virtual float CalculateDynamicCost(GPlanParameters parameters)
        {
            return 0;
        }

        public virtual void Canceled()
        {
        }

        void IGActionBase.ForceComplete(bool success)
        {
            _Complete(success);
        }

        public virtual void Planned()
        {
        }

        bool IGActionBase.Enter()
        {
            if (IsActive)
                return false;

            State = StateState.Entering;
            OnEnter();
            //In case completed in enter
            if (State != StateState.Entering)
                return true;

            State = StateState.Active;

            Owner.Log($"Enter [{ActionName}]");
            OnEntered?.Invoke();
            return true;
        }

        void IGActionBase.Exit()
        {
            if (!IsActive)
                return;

            _delayedCall?.TryDisposeAsOwner(this);
            State = StateState.Exiting;
            OnExit();
            State = StateState.None;
            OnExited?.Invoke();
        }

        //
        //     protected GPlanParameters Parameters => Agent.PlanParameters;
        public float GetCost(GPlanParameters parameters)
        {
            return Cost + CalculateDynamicCost(parameters);
        }

        public virtual bool IsValid(GPlanParameters parameters)
        {
            return true;
        }

        public virtual void SetParameters(GPlanParameters parameters)
        {
        }

        private void _Complete(bool success)
        {
            OnCompleting(success);
            Owner.Log($"Action [{ActionName}] {(success ? " " + nameof(success) : " fail")}");
            OnCompleted?.Invoke(success);
        }
    }
}