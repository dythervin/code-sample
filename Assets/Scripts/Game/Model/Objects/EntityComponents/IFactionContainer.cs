using Game.Data;
using UnityEngine;

namespace Game
{
    public interface IFactionContainer
    {
        Material Get(Faction faction);
    }
}