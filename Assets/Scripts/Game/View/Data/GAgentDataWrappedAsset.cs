using Game.Data;
using UnityEngine;

namespace Game.View.Data
{
    [System.Serializable]
    [CreateAssetMenu(menuName = MenuName + "GAgent")]
    public class GAgentDataWrappedAsset : EntityComponentDataWrappedAsset<GAgentData>
    {
    }
}