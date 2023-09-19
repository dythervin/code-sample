using Dythervin.Game.Framework;
using Game.Data;
using Game.Items;

namespace Game.AI
{
    public class HitRangedAgentAction : HitBaseAgentAction<HitRangedActionData, IWeaponRanged>
    {
        protected override float Range => DamageTypeExt.DefaultRangedRange;

        public HitRangedAgentAction(HitRangedActionData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }

        protected override bool TryTriggerInternal()
        {
            Weapon.Trigger(Parameters.target.Center);
            return true;
        }

        protected override bool IsWeaponValid(IWeaponRanged weapon)
        {
            return weapon.Data.DataAsset.Range > DamageTypeExt.DefaultRangedRange;
        }
    }
}