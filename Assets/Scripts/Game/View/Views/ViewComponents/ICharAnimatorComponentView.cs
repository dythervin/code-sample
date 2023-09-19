using Dythervin.Game.Framework;

namespace Game.View.ViewComponents
{
    public interface ICharAnimatorComponentView : IObject
    {
        void TriggerRandomIdle();

        void ExitIdle();

        void SetRagdoll(bool value);
    }
}