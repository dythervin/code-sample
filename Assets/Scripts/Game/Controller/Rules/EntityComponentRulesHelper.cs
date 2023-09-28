using System.Collections.Generic;
using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Controller;
using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;
using Game.Data;
using Game.View;
using Zenject;

namespace Game.Controller.Rules
{
    public class EntityComponentRulesHelper : GameRulesHelper
    {
        private EntityComponentViewFactory _defaultEntityComponentViewFactory;

        protected override void Init(DiContainer container)
        {
            base.Init(container);
            _defaultEntityComponentViewFactory =
                new EntityComponentViewFactory(GameObjectFactory, ViewMap, ViewContext);
        }

        public IReadOnlyList<FeatureComponentFactoryView<EntityComponentFactory<TObject, TData>, TObject, TData>>
            Construct<TObject, TData>(params IFeatureParameter[] featureParameters)
            where TData : class, IEntityComponentDataExt, new()
            where TObject : class, IEntityComponentExt, IModelInitializable
        {
            return new[]
            {
                ConstructInternal<TObject, TData>(featureParameters)
                //ConstructInternal<TObject, WrappedEntityComponentData<TData>>(featureParameters),
            };
        }

        private FeatureComponentFactoryView<EntityComponentFactory<TObject, TData>, TObject, TData>
            ConstructInternal<TObject, TData>(params IFeatureParameter[] featureParameters)
            where TData : class, IEntityComponentDataExt, new()
            where TObject : class, IEntityComponentExt, IModelInitializable
        {
            var factoryController =
                new FeatureComponentFactoryView<EntityComponentFactory<TObject, TData>, TObject, TData>(
                    new EntityComponentFactory<TObject, TData>(Context),
                    ViewContext,
                    Features.GetDataGroupId<IEntityComponentData>(),
                    featureParameters);

            factoryController.SetViewFactory(_defaultEntityComponentViewFactory);
            return RuleFactory.Register(factoryController);
        }
    }
}