using Dythervin.Game.Framework;
using Game.AI;
using Game.View.ViewComponents;

namespace Game.View.GOAP
{
    public class IdleAgentActionView<TObserver> : GAgentActionView<TObserver>
        where TObserver : class, IGAgentAction, IModel
    {
        private ICharAnimatorComponentView _animator;

        protected override void Init()
        {
            base.Init();
            Owner.Owner.ViewComponents.TryGet(out _animator);
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            _animator.TriggerRandomIdle();
        }

        protected override void OnExit()
        {
            base.OnExit();
            _animator.ExitIdle();
        }
    }


    public class IdleAgentActionView : IdleAgentActionView<IdleAgentAction> { }
}