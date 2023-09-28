using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Controller;
using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;
using Game.Data;
using Game.GameComponents;
using Zenject;

namespace Game.Controller.Rules
{
    public class GameComponentRulesHelper : GameRulesHelper
    {
        private ComponentViewFactory<IGameComponentExt, IGameComponentView> _defaultGameComponentViewFactory;

        protected override void Init(DiContainer container)
        {
            base.Init(container);
            _defaultGameComponentViewFactory =
                new ComponentViewFactory<IGameComponentExt, IGameComponentView>(GameObjectFactory,
                    ViewMap,
                    ViewContext);
        }

        public FeatureComponentFactoryView<GameComponentFactory<TObject, TData>, TObject, TData>
            Construct<TObject, TData>(params IFeatureParameter[] featureParameters)
            where TData : class, IGameComponentDataExt, new()
            where TObject : class, IModelInitializable, IGameComponentExt
        {
            var factoryController =
                new FeatureComponentFactoryView<GameComponentFactory<TObject, TData>, TObject, TData>(
                    new GameComponentFactory<TObject, TData>(Context),
                    ViewContext,
                    Features.GetDataGroupId<IGameComponentData>(),
                    featureParameters);

            factoryController.SetViewFactory(_defaultGameComponentViewFactory);
            return RuleFactory.Register(factoryController);
        }
    }
}