using Dythervin.Game.Framework;
using Dythervin.Game.Framework.View;
using Dythervin.Serialization;
using Game.Data;
using Game.View;
using Zenject;

namespace Game.Controller.Rules
{
    public interface IGameRulesContextExt : IGameRulesContext
    {
        void Init(IGameRules defaultGameRules);
    }

    public class GameRulesContextExt : IGameRulesContextExt
    {
        public IModelContext Context { get; }

        public IDataAssetRepository AssetRepository { get; }

        public IRuleRepository RuleRepository { get; }

        private readonly IGameRulesHelperInitializer[] _gameRules;

        [Inject] private IGameObjectFactory _gameObjectFactory;

        [Inject] private IViewMap _viewMap;

        [Inject] private IViewContext _viewContext;

        public EntityRulesHelper EntityRulesHelper { get; }

        public EntityComponentRulesHelper EntityComponentRulesHelper { get; }

        public GameComponentRulesHelper GameComponentRulesHelper { get; }

        public GAgentActionRulesHelper GAgentActionRulesHelper { get; }

        public GameRulesContextExt(IModelContextExt context, IRuleRepository ruleRepository,
            IDataAssetRepository assetRepository)
        {
            Context = context;
            RuleRepository = ruleRepository;
            AssetRepository = assetRepository;
            EntityRulesHelper = new EntityRulesHelper();
            EntityComponentRulesHelper = new EntityComponentRulesHelper();
            GameComponentRulesHelper = new GameComponentRulesHelper();
            GAgentActionRulesHelper = new GAgentActionRulesHelper();

            _gameRules = new IGameRulesHelperInitializer[]
            {
                EntityRulesHelper, EntityComponentRulesHelper, GameComponentRulesHelper, GAgentActionRulesHelper
            };

            foreach (IGameRulesHelperInitializer gameRules in _gameRules)
            {
                gameRules.Init(ruleRepository, context);
            }
        }

        [Inject]
        public void Bind(DiContainer container)
        {
            foreach (IGameRulesHelperInitializer gameRules in _gameRules)
            {
                container.Inject(gameRules);
            }
        }

        public void Init(IGameRules rules)
        {
            RuleRepository.Clear();
            rules.Construct(this, GameComponentRulesHelper);
            rules.Construct(this, EntityComponentRulesHelper);
            rules.Construct(this, GAgentActionRulesHelper);
            rules.Construct(this, EntityRulesHelper);
            
            TypeSerializationByGuid.Register(typeof(ModelData).Assembly);
            TypeSerializationByGuid.Register(typeof(EntityDataExt).Assembly);
            TypeSerializationByGuid.Register(typeof(WrappedEntityComponentData).Assembly);
        }

        public ViewFactory<TModel, TView> DefaultViewFactory<TModel, TView>()
            where TModel : class, IModel
            where TView : class, IModelView
        {
            return ViewFactory<TModel, TView>.Construct<TModel>(_gameObjectFactory, _viewMap, _viewContext);
        }
    }
}