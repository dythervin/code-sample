using Dythervin.Core;

namespace Game
{
    public interface IDamagePreprocessor : IPrioritized
    {
        void Preprocess(ref Common.Damage damage);
    }
}