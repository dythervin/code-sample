using Dythervin.Game.Framework;
using Dythervin.Routines;
using Game.Data;

namespace Game.AI
{
    public abstract class TempGAgentAction<TData> : GAgentAction<TData>
        where TData : class, ITempGActionData
    {
        private readonly RoutineSeq _routine = new();
        private readonly WaitForSecondsInstr _waitForSecondsInstr;
        private float _duration;

        public float Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                _waitForSecondsInstr.Seconds = value;
            }
        }

        protected TempGAgentAction(TData data, IModelContextExt context, IModelConstructorContext constructorContext) : base(data, context, constructorContext) 
        {
            _duration = data.Duration;
            _waitForSecondsInstr = _routine.Append(new WaitForSecondsInstr(_duration));
            _routine.Append(() => Complete(true));
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            _routine.Start();
        }

        protected override void OnExit()
        {
            base.OnExit();
            _routine.Stop();
        }
    }
}