using System;
using Dythervin.Game.Framework;
using Game.Data;
using Game.Items;

namespace Game.AI
{
    public class HitMeleeAgentAction : HitBaseAgentAction<HitMeleeActionData, IWeapon>
    {
        protected override float Range => DamageTypeExt.DefaultMeleeRange;

        public HitMeleeAgentAction(HitMeleeActionData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }

        protected override bool TryTriggerInternal()
        {
            throw new NotImplementedException();
        }

        protected override bool IsWeaponValid(IWeapon weapon)
        {
            return weapon.Data.DataAsset.Range <= DamageTypeExt.DefaultMeleeRange;
        }
    }
}