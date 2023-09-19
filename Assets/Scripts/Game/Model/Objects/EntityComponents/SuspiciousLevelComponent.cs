using System;
using Dythervin.AI.GOAP;
using Dythervin.Game.Framework;
using Game.Data;

namespace Game
{
    public class SuspiciousLevelComponent : EntityComponentExt<SuspiciousLevelData>, ISuspiciousLevelComponent
    {
        public event Action<SuspiciousLevel> OnChanged;

        private SuspiciousLevel _suspiciousLevel = SuspiciousLevel.Calm;

        public SuspiciousLevel SuspiciousLevel => _suspiciousLevel;

        public void UpdateLevel(SuspiciousLevel value)
        {
            if (value == _suspiciousLevel)
                return;

            _suspiciousLevel = value;
            OnChanged?.Invoke(_suspiciousLevel);
        }

        public SuspiciousLevelComponent(SuspiciousLevelData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }
    }
}