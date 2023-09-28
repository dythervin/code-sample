using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Controller;
using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;
using Game.AI;
using Game.Data;
using Game.View.GOAP;
using Zenject;

namespace Game.Controller.Rules
{
    public class GAgentActionRulesHelper : GameRulesHelper
    {
        private ComponentViewFactory<IGAgentAction, IGAgentActionView> _gAgentComponentViewFactory;

        protected override void Init(DiContainer container)
        {
            base.Init(container);
            _gAgentComponentViewFactory =
                new ComponentViewFactory<IGAgentAction, IGAgentActionView>(GameObjectFactory, ViewMap, ViewContext);
        }

        public FeatureComponentFactoryView<GActionFactory<TObject, TData>, TObject, TData>
            Construct<TObject, TData>(params IFeatureParameter[] featureParameters)
            where TData : class, IGAgentActionData, new()
            where TObject : class, IGAgentAction, IModelInitializable
        {
            var factoryController =
                new FeatureComponentFactoryView<GActionFactory<TObject, TData>, TObject, TData>(
                    new GActionFactory<TObject, TData>(Context),
                    ViewContext,
                    Features.GetDataGroupId<IGAgentActionData>(),
                    featureParameters);

            factoryController.SetViewFactory(_gAgentComponentViewFactory);
            return RuleFactory.Register(factoryController);
        }
    }
}