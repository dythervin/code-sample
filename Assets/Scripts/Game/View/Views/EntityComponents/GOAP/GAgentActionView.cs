using Dythervin.Game.Framework.View;
using Game.AI;

namespace Game.View.GOAP
{
    public abstract class GAgentActionView<TGAgentAction>
        : ModelComponentView<IGAgentComponentView, TGAgentAction, IViewContextExt, IViewComponentExt>,
            IGAgentActionView
        where TGAgentAction : class, IGAgentAction
    {
        public IGAgentAction Data => Model;

        IGAgentAction IGAgentActionView.Model => Model;

        IGAgentAction IModelView<IGAgentAction>.Model => Model;

        IGAgentComponentView IGAgentActionView.Owner => Owner;

        IModelView IModelComponentView.Owner => Owner;

        protected override void Init()
        {
            base.Init();
            Model.OnEntered += OnEnter;
            Model.OnExited += OnExit;
        }

        protected virtual void OnExit() { }

        protected virtual void OnEnter() { }
    }
}