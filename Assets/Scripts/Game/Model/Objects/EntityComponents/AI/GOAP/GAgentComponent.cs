using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Dythervin.AI.GOAP;
using Dythervin.AI.GOAP.Simple;
using Dythervin.Collections;
using Dythervin.Common;
using Dythervin.Core.Extensions;
using Dythervin.Game.Framework;
using Dythervin.ObjectPool;
using Dythervin.Routines;
using Game.AI.GOAP;
using Game.Data;
using Zenject;

namespace Game.AI
{
    //TODO: Implement IController instead of IModel
    [DebuggerDisplay("Action: {_currentAction}")]
    public abstract class
        GAgentComponent<TGAgentData> : ModelComponent<IEntityExt, IModelContextExt, TGAgentData, IGAgentAction>,
            IGAgentComponent
        where TGAgentData : class, IGAgentData, IEntityComponentDataExt
    {
        public event Action OnGotPlan;

        public event Action<GPlanParameters> OnPlanning;

        /// <summary>
        ///     Action result
        /// </summary>
        protected readonly Dictionary<StateValue, IReadOnlyList<byte>> resultActionMap = new();

        protected readonly Dictionary<SuspiciousLevel, IReadOnlyList<StateValue>> goalsMap = new();

        protected readonly List<IGAgentAction> plan = new();

        private IGAgentAction _currentAction;

        private GPlanParameters _planParameters;

        private IGAgentStateComponent _state;

        private DiContainer _diContainer;

        private IDelayedCall _delayedCall;

        private bool AutoRequest => true;

        public GPlanParameters PlanParameters => _planParameters;

        protected IGAgentStateComponent State => _state;

        protected abstract SuspiciousLevel SuspiciousLevel { get; }

        protected virtual bool CanAct => true;

        IEntity IEntityComponent.Owner => Owner;

        protected GAgentComponent(TGAgentData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
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
        }

        protected virtual void Inject(DiContainer diContainer)
        {
            foreach (IGAgentAction component in components.ToEnumerable())
            {
                diContainer.Inject(component);
            }
        }

        protected override void Init()
        {
            base.Init();
            Owner.GetComponent(out _state);

            byte actionIndex = 0;

            foreach (IGAgentAction action in components)
            {
                action.OnCompleted += OnCompleted;
                var actionResult = action.Result;
                for (int j = 0; j < actionResult.Count; j++)
                {
                    StateValue stateValue = actionResult[j].state;
                    if (!resultActionMap.TryGetValue(stateValue, out var list))
                    {
                        resultActionMap[stateValue] = list = new List<byte>();
                    }

                    ((IList<byte>)list).Add(actionIndex);
                }

                actionIndex++;
            }

            InitGoals();
        }

        protected override void LateInit()
        {
            base.LateInit();
            TryRequestPlanAndAct();
        }

        protected virtual void PlanRequested(bool found)
        {
        }

        protected override void ComponentAdded(IGAgentAction component)
        {
            _diContainer.Inject(component);
            base.ComponentAdded(component);
            component.OnCompleted += OnCompleted;
        }

        protected override void ComponentRemoved(IGAgentAction component)
        {
            base.ComponentRemoved(component);
            component.OnCompleted -= OnCompleted;
        }

        private void Next()
        {
            if (!CanAct || !TryGetNextAction())
            {
                TryRequestPlanAndAct();
            }
        }

        private bool TryGetNextAction()
        {
            return PopFromPlan(out _currentAction) && _currentAction.Enter();
        }

        private void OnCompleted(bool success)
        {
            if (_currentAction is not { State: StateState.Active or StateState.Entering })
            {
                throw new Exception();
            }

            _currentAction.Exit();

            if (success)
            {
                OnSuccess(_currentAction);

                if (TryGetNextAction())
                {
                    return;
                }
            }

            TryRequestPlanAndAct();
        }

        public void TryRequestPlanAndAct(float delay = 0)
        {
            if (delay > 0)
            {
                if (_delayedCall == null || _delayedCall.IsDisposed)
                {
                    _delayedCall = DelayedCaller.Add(this,
                        delay,
                        (action) =>
                        {
                            action._delayedCall = null;
                            action.TryRequestPlanAndAct();
                        });
                }

                return;
            }

            if (!RequestPlan())
            {
                if (AutoRequest)
                    TryRequestPlanAndAct(1);

                return;
            }

            Next();
        }

        private void InitGoals()
        {
            var suspiciousLevels = SuspiciousLevel.None.GetValues(true);
            using var handler = ListPools<StateValue>.Shared.Get(out var temp);
            for (int index = 0; index < suspiciousLevels.Count; index++)
            {
                SuspiciousLevel suspiciousLevel = suspiciousLevels[index];
                temp.Clear();

                for (int i = 0; i < Data.Goals.Count; i++)
                {
                    StateValueGoal goal = Data.Goals[i];
                    if (goal.suspiciousLevel.HasFlagFast(suspiciousLevel))
                    {
                        temp.Add(goal.state);
                    }
                }

                if (temp.Count > 0)
                {
                    goalsMap[suspiciousLevel] = temp.ToArray();
                    temp.Clear();
                }
            }
        }

        private bool PopFromPlan(out IGAgentAction action)
        {
            if (plan.TryPopLast(out IGAgentAction tAction))
            {
                action = tAction;
                return true;
            }

            action = null;
            return false;
        }

        private void OnSuccess(IGAgentAction action)
        {
            for (int i = 0; i < action.Result.Count; i++)
            {
                StateValueResult result = action.Result[i];
                if (result.isPersistent)
                {
                    StateValue state = result.state;
                    State[state.key] = state.value;
                }
            }
        }

        private void ClearPlan()
        {
            foreach (IGAgentAction gAction in plan)
            {
                gAction.Canceled();
            }

            plan.Clear();
        }

        private bool RequestPlanInner()
        {
            CancelPlan();
            bool found = RequestPlanInternal(out float cost);
            this.Log(found ?
                $"Plan found: {string.Join(", ", plan.Select(x => x.ActionName))}, cost: {cost:N}" :
                "Plan failed");

            if (found)
            {
                foreach (IGAgentAction gAction in plan)
                {
                    gAction.Planned();
                }
            }

            return found;
        }

        private bool RequestPlanInternal(out float cost)
        {
            var planner = GPlannerSystemMapped<IGAgentAction, GPlanParameters>.Instance;

            if (_planParameters != null)
            {
                _planParameters.Clear();
            }
            else
            {
                _planParameters = planner.planParametersPool.Get();
            }

            OnPlanning?.Invoke(_planParameters);

            return planner.GetPlan(plan,
                ref _planParameters,
                components,
                resultActionMap,
                goalsMap[SuspiciousLevel],
                State,
                out cost);
        }

        public bool RequestPlan()
        {
            if (!CanAct)
            {
                if (_currentAction != null && _currentAction.State != StateState.None)
                    throw new Exception("Active action");

                return false;
            }

            bool found = RequestPlanInner();
            PlanRequested(found);
            if (found)
            {
                OnGotPlan?.Invoke();
            }

            return found;
        }

        protected void CancelPlan()
        {
            _delayedCall?.TryDisposeAsOwner(this);
            _currentAction?.Exit();
            ClearPlan();
        }

        void IEntityComponent.SetOwner(IEntity owner)
        {
            SetOwner((IEntityExt)owner);
        }
    }
}