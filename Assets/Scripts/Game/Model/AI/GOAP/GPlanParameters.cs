using System.Collections.Generic;
using Dythervin.AI.GOAP;
using Game.AI.PathfindingExt;
using Game.Items;

namespace Game.AI.GOAP
{
    //TODO: make more generic
    public class GPlanParameters : IGPlanParameters<GPlanParameters>
    {
        public readonly List<Destination> destinations = new();
        public IEntityExt target;
        public IWeapon weaponData;

        public void CopyTo(GPlanParameters other)
        {
            other.Clear();

            foreach (Destination destination in destinations)
                other.destinations.Add(destination);

            other.weaponData = weaponData;
            other.target = target;
        }

        public void Clear()
        {
            target = null;
            destinations.Clear();
        }
    }
}