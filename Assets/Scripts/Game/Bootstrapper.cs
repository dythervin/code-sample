using Dythervin.Common;
using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Controller;
using Dythervin.Game.Framework.View;
using Game.Controller.Rules;
using Game.View;
using Zenject;

namespace Game
{
    public class Bootstrapper : MonoInstaller
    {
        private readonly IServiceContainer _serviceContainer = new ServiceContainer();

        public override void InstallBindings()
        {
            Services.Init(_serviceContainer);
            _serviceContainer.OnAdded += ServiceContainerOnOnAdded;

            Container.BindInterfacesAndSelfTo<IServiceContainer>().FromInstance(_serviceContainer);
            AssetKeyResolverService assetKeyResolverService = new AssetKeyResolverService();
            _serviceContainer.AddService(assetKeyResolverService);
            AssetsDatabaseService assetsDatabaseService = new AssetsDatabaseService();
            _serviceContainer.AddService(assetsDatabaseService);
            //_serviceContainer.AddService(new ItemDatabaseService().LoadDatabase());

            EntityColliderMap entityColliderMap = new EntityColliderMap();
            Container.Bind<IEntityColliderMap>().FromInstance(entityColliderMap).AsSingle();

            EntityExtensions.Init(entityColliderMap);

            RuleRepository ruleRepository = new RuleRepository();
            Container.BindInterfacesAndSelfTo<RuleRepository>().FromInstance(ruleRepository);
            ruleRepository.OnAdded += factory => Container.Inject(factory);

            ViewMap viewMap = new ViewMap();
            Container.Bind<IViewMap>().FromInstance(viewMap).AsSingle();

            IControllerMap controllerMap = new ControllerMap();
            Container.Bind<IControllerMap>().FromInstance(controllerMap).AsSingle();

            GameObjectFactoryExt gameObjectFactory =
                new GameObjectFactoryExt(assetsDatabaseService, assetKeyResolverService);

            Container.Bind<IGameObjectFactory>().FromInstance(gameObjectFactory).AsSingle();

            DataAssetRepository dataAssetRepository = new DataAssetRepository(assetsDatabaseService);
            Container.Bind<IDataAssetRepository>().FromInstance(dataAssetRepository).AsSingle();

            AnyFeatureFactoryExt anyFeatureFactory = new AnyFeatureFactoryExt(ruleRepository);
            Container.Bind<IAnyFactory>().FromInstance(anyFeatureFactory).AsSingle();

            ViewContextExt viewContextExt = new ViewContextExt(gameObjectFactory, viewMap);
            Container.Bind<IViewContext>().FromInstance(viewContextExt).AsSingle();

            AnyViewFactoryExt anyViewFactoryExt = new AnyViewFactoryExt(gameObjectFactory, viewMap);
            Container.Bind<IAnyViewFactory>().FromInstance(anyViewFactoryExt).AsSingle();

            GameControllerExt gameController =
                new GameControllerExt(anyFeatureFactory, dataAssetRepository, _serviceContainer);

            Container.BindInterfacesAndSelfTo<GameControllerExt>().FromInstance(gameController).AsSingle();
            GameRulesContextExt gameRulesContextExt =
                new GameRulesContextExt(gameController, ruleRepository, dataAssetRepository);

            Container.BindInterfacesAndSelfTo<GameRulesContextExt>().FromInstance(gameRulesContextExt).AsSingle();
            Container.Inject(gameRulesContextExt);
            Container.Inject(gameController);
        }

        private void ServiceContainerOnOnAdded(IService obj)
        {
            Container.BindInterfacesAndSelfTo(obj.GetType()).FromInstance(obj).AsSingle();
        }
    }
}