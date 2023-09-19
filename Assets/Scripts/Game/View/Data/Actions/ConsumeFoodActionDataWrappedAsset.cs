using Game.Data;
using UnityEngine;

namespace Game.View.Data.Actions
{
    [CreateAssetMenu(menuName = MenuName + nameof(ConsumeFoodActionData))]
    public class ConsumeFoodActionDataWrappedAsset : ActionDataWrappedAsset<ConsumeFoodActionData>
    {
    }
}