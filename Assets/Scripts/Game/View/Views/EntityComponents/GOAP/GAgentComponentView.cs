using Dythervin.Game.Framework;
using Dythervin.Game.Framework.View;
using Game.AI;
using Sirenix.OdinInspector;


namespace Game.View.GOAP
{
    public class GAgentComponentView : ModelComponentViewWithComponents<IEntityViewExt, IGAgentComponent, IViewContextExt, IViewComponentExt>,
            IGAgentComponentView
    {
        IEntityComponent IEntityComponentView.Model => Model;

        IEntityView IEntityComponentView.Owner => Owner;

        IEntityViewExt IEntityComponentViewExt.Owner => Owner;

        IModelView IModelComponentView.Owner => Owner;

        [Button]
        [HideInEditorMode]
        public void TryRequestPlanAndAct()
        {
            Model.TryRequestPlanAndAct();
        }
    }
}