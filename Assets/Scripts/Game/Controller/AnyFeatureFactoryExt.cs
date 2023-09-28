using Dythervin.Game.Framework;
using Zenject;

namespace Game
{
    public class AnyFeatureFactoryExt : AnyFeatureFactory
    {
        [Inject]
        public AnyFeatureFactoryExt(IRuleFactoryMap featureFactoryMap) : base(featureFactoryMap)
        {
        }
    }
}