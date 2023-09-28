using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Controller;
using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;
using Game.View;
using Zenject;

namespace Game.Controller.Rules
{
    public class EntityRulesHelper : GameRulesHelper
    {
        private EntityViewFactory _defaultEntityViewFactory;

        protected override void Init(DiContainer container)
        {
            base.Init(container);
            _defaultEntityViewFactory = new EntityViewFactory(GameObjectFactory, ViewMap, ViewContext, container, null);
        }

        public FeatureFactoryController<EntityExtFactory<TObject, TData>, TObject, TData> Construct<TObject, TData>(
            params IFeatureParameter[] featureParameters)
            where TData : class, IEntityROData, new()
            where TObject : class, IEntityExt, IModelInitializable
        {
            var factoryController = new FeatureFactoryController<EntityExtFactory<TObject, TData>, TObject, TData>(
                new EntityExtFactory<TObject, TData>(Context),
                ViewContext,
                Features.GetDataGroupId<IEntityROData>(),
                featureParameters);

            factoryController.SetViewFactory(_defaultEntityViewFactory);
            return RuleFactory.Register(factoryController);
        }
    }
}