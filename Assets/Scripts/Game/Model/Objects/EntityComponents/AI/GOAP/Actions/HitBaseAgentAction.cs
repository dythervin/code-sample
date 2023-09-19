using Dythervin.Game.Framework;
using Dythervin.Routines;
using Dythervin.UpdateSystem;
using Game.AI.GOAP;
using Game.AI.PathfindingExt;
using Game.Data;
using Game.Items;
using Game.Items.Inventory;

namespace Game.AI
{
    public abstract class HitBaseAgentAction<TData, TWeapon> : GAgentAction<TData>, IUpdatableDelta
        where TData : class, IGAgentActionData
        where TWeapon : class, IWeapon
    {
        private IInventory _inventory;

        private ITargetAimComponent _aimComponent;

        private IEntityEquipment _entityEquipment;

        private IHealthComponent _targetHealth;

        protected abstract float Range { get; }

        public TWeapon Weapon { get; private set; }

        public IEntityExt Target { get; private set; }

        public IHealthComponent TargetHealth => _targetHealth;

        protected override void Init()
        {
            base.Init();
            Owner.Owner.GetComponent(out _inventory);
            Owner.Owner.GetComponent(out _entityEquipment);
            Owner.Owner.GetComponent(out _aimComponent);
            _aimComponent.OnAimed += AimComponentOnOnAimed;
            _aimComponent.OnLost += AimComponentOnOnLost;

            _inventory.OnAdded += InventoryOnOnAdded;
            GetMostExpensiveWeapon();
        }

        private void AimComponentOnOnLost()
        {
            if (!IsActive)
            {
                return;
            }

            this.DisableUpdater();
            Complete(false);
        }

        private void AimComponentOnOnAimed(AimData aimData)
        {
            if (!IsActive)
            {
                return;
            }

            this.EnableUpdater();
        }

        private void InventoryOnOnAdded(IItem item)
        {
            GetMostExpensiveWeapon();
        }

        public override void Planned()
        {
            base.Planned();
            if (Parameters.weaponData is { IsEquipped: false })
            {
                _entityEquipment.TryEquip(Parameters.weaponData);
            }

            Target = Parameters.target;
            Target.GetComponent(out _targetHealth);
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            _aimComponent.AimTarget(Parameters.target, Weapon.Range);
        }

        protected override void OnExit()
        {
            //_delayedCall?.TryDisposeAsOwner(this);
            _aimComponent.Stop();
            base.OnExit();
        }

        private bool CanContinue => IsActive && _targetHealth != null && _targetHealth.IsAlive;

        void IUpdatableDelta.OnUpdate(float deltaTime)
        {
            if (!CanContinue)
            {
                Complete(false);
            }

            float cooldown = Weapon.Cooldown;
            if (cooldown > 0)
            {
                return;
            }

            if (TryTriggerInternal())
            {
                
                if (!TargetHealth.IsAlive)
                {
                    Complete(true);
                }
            }
            else
            {
                Complete(false);
            }
        }

        protected abstract bool TryTriggerInternal();

        public override bool IsValid(GPlanParameters parameters)
        {
            if (Weapon != null && base.IsValid(parameters))
            {
                return true;
            }

            //Owner.Log("No weapon");
            return false;
        }

        public override void SetParameters(GPlanParameters parameters)
        {
            base.SetParameters(parameters);
            parameters.weaponData = Weapon;
            parameters.destinations.Add(new Destination(parameters.target, true, Range));
        }

        protected abstract bool IsWeaponValid(TWeapon weapon);

        private void GetMostExpensiveWeapon()
        {
            float mostExpensive = -1;
            Weapon = null;
            foreach (IItem item in _inventory)
            {
                if (item is not TWeapon weapon)
                {
                    continue;
                }

                if (!weapon.Data.DataAsset.Slots.ContainsInItems(EquipmentSlots.MainHand) || !IsWeaponValid(weapon))
                {
                    continue;
                }

                float price = weapon.Data.DataAsset.Price;
                if (price > mostExpensive)
                {
                    mostExpensive = price;
                    Weapon = weapon;
                }
            }
        }

        protected HitBaseAgentAction(TData data, IModelContextExt context, IModelConstructorContext constructorContext)
            : base(data, context, constructorContext)
        {
        }
    }
}