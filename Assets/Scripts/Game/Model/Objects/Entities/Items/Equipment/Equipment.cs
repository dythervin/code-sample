using System;
using Dythervin.Game.Framework;
using Game.Data;

namespace Game.Items
{
    [Serializable]
    public abstract class Equipment<T> : Item<T>, IEquipment
        where T : class, IEquipmentData
    {
        public event Action OnEquipChanged;

        IEquipmentROData IEquipment.Data => Data;

        public int Level
        {
            get => Data.Level;
            set => Data.Level = value;
        }

        public bool IsEquipped
        {
            get => Data.IsEquipped;
            set
            {
                if (IsEquipped == value)
                    return;

                Data.IsEquipped = value;
                MarkDataDirty();
                OnEquipChanged?.Invoke();
            }
        }

        protected Equipment(T data, IModelContextExt context, IModelConstructorContext constructorContext) : base(
            data,
            context,
            constructorContext)
        {
        }
    }
}