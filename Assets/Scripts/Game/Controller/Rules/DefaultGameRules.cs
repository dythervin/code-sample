using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Controller;
using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;
using Game.AI;
using Game.Data;
using Game.Game;
using Game.GameComponents.Faction;
using Game.GameComponents.Radar;
using Game.Inventory;
using Game.Items;
using Game.Items.Inventory;
using Game.View;
using Game.View.AI;

namespace Game.Controller.Rules
{
    public interface IGameRules : IGameRules<GameComponentRulesHelper>,
        IGameRules<EntityComponentRulesHelper>,
        IGameRules<GAgentActionRulesHelper>,
        IGameRules<EntityRulesHelper>
    {}

    public class DefaultGameRules : IGameRules
    {
        public void Construct(IGameRulesContext context, GameComponentRulesHelper helper)
        {
            helper.Construct<EntityRadarGameComponent, EntityRadarGameComponentData>()
                .AddFastTypeAccess<IEntityRadarGameComponent>();

            helper.Construct<FactionRelationsGameComponent, FactionRelationsGameComponentData>()
                .AddFastTypeAccess<IFactionRelationsGameComponent>();
        }

        public void Construct(IGameRulesContext context, EntityComponentRulesHelper helper)
        {
            helper.Construct<HealthComponent, HealthComponentData>().AddFastTypeAccess<IHealthComponent>();
            helper.Construct<AIMovementComponent, AIMovementComponentData>()
                .AddFastTypeAccess<INavMovementComponent, IAnyMovementComponent>();

            helper.Construct<FactionComponent, FactionComponentData>(context.AssetRepository.LoadAsset<FactionContainer>()).AddFastTypeAccess<IFactionComponent>();

            helper.Construct<ResistanceComponent, ResistanceComponentData>().AddFastTypeAccess<IResistanceComponent>();

            helper.Construct<StatusEffectComponent, StatusEffectComponentData>()
                .AddFastTypeAccess<IStatusEffectComponent>();

            helper.Construct<StunEffectComponent, StunEffectComponentData>().AddFastTypeAccess<IStunEffectComponent>();

            helper.Construct<DamagableComponent, DamagableComponentData>()
                .AddFastTypeAccess<IDamagable, IDamagableComponent, IDamagePreprocessorContainerComponent>();

            helper.Construct<AISightComponent, SightControllerData>().AddFastTypeAccess<IAISightComponent>();
            helper.Construct<SuspiciousLevelComponent, SuspiciousLevelData>()
                .AddFastTypeAccess<SuspiciousLevelComponent>();

            helper.Construct<EntityEquipmentComponent, EntityEquipmentComponentData>()
                .AddFastTypeAccess<IEntityEquipment>();

            // helper.Construct<EquipmentInventoryWeapon, EquipmentWeaponInventoryComponent>()
            //     .AddFastTypeAccess<ICharacterWeapon>();
            helper.Construct<InventoryComponent, InventoryData>().AddFastTypeAccess<IInventory>();

            helper.Construct<LogComponent, LogComponentData>().AddFastTypeAccess<ILogComponent>();
            helper.Construct<EntityRadarComponent, EntityRadarData>().AddFastTypeAccess<IEntityRadarComponent>();
            helper.Construct<GAgentComponentExt, GAgentData>().AddFastTypeAccess<IGAgentComponent>();
            helper.Construct<GAgentStateComponent, GAgentStateComponentData>()
                .AddFastTypeAccess<IGAgentStateComponent>();

            helper.Construct<TargetAimComponent, TargetAimComponentData>().AddFastTypeAccess<ITargetAimComponent>();
        }

        public void Construct(IGameRulesContext context, GAgentActionRulesHelper helper)
        {
            helper.Construct<ConsumeFoodAgentAction, ConsumeFoodActionData>();
            helper.Construct<GoToAgentAction, GoToActionData>();
            helper.Construct<HitMeleeAgentAction, HitMeleeActionData>();
            helper.Construct<HitRangedAgentAction, HitRangedActionData>();
            helper.Construct<IdleAgentAction, IdleGActionData>();
            helper.Construct<PatrolAgentAction, PatrolGActionData>();
            helper.Construct<WorkAgentAction, WorkActionData>();
        }

        public void Construct(IGameRulesContext context, EntityRulesHelper helper)
        {
            helper.Construct<Character, CharacterData>();
            helper.Construct<CameraEntity, CameraEntityData>();
            helper.Construct<WeaponRanged, WeaponRangedData>();
            helper.Construct<Armor, ArmorData>().SetViewFactory(null);
            helper.Construct<ItemStackable, ItemStackableData>().SetViewFactory(null);

            context.RuleRepository.Register(
                new FeatureFactoryController<GameFactoryExt<GameExt, GameData>, GameExt, GameData>(
                    new GameFactoryExt<GameExt, GameData>(context.Context, null),
                    helper.ViewContext,
                    Features.GetDataGroupId<IGameData>(),
                    null).SetViewFactory(context.DefaultViewFactory<GameExt, GameViewExt>()));
        }
    }
}