using UnityEngine;
using CharacterData = Game.Data.CharacterData;

namespace Game.View.Data
{
    [System.Serializable]
    [CreateAssetMenu(menuName = MenuName + nameof(Character))]
    public class CharacterDataWrappedAsset : EntityDataWrappedAsset<CharacterData>
    {
    }
}