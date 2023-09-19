using System;
using Dythervin.Game.Framework;
using Game.Common;
using Game.Data;

namespace Game.Items
{
    public class Armor : Equipment<ArmorData>, IResistanceProvider, IArmor
    {
        public event Action OnResistanceChanged;

        public int this[DamageType key] => 0;

        //Data.Resistances.TryGetValue(key, out FloatField value) ? value.EvaluateInt(Owner.VarResolver) : 0;

        public Armor(ArmorData data, IModelContextExt context, IModelConstructorContext constructorContext) :
            base(data, context, constructorContext)
        {
        }

        IArmorData IArmor.Data => Data;
    }
}