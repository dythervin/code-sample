using System;
using System.Collections.Generic;
using Dythervin.Game.Framework;
using Game.Data;

namespace Game.AI
{
    public class GAgentStateComponent : EntityComponentExt<GAgentStateComponentData>, IGAgentStateComponent
    {
        public event Action OnChanged;

        private readonly Dictionary<ushort, short> _states = new Dictionary<ushort, short>();

        public GAgentStateComponent(GAgentStateComponentData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
        }

        public short this[ushort id]
        {
            get => _states[id];
            set
            {
                _states[id] = value;
                OnChanged?.Invoke();
            }
        }

        public bool TryGetValue(ushort key, out short value)
        {
            return _states.TryGetValue(key, out value);
        }
    }
}