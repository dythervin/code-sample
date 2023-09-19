using System.Collections.Generic;
using Dythervin.Game.Framework;

namespace Game.Items.Inventory
{
    public interface IInventory : IEntityComponent
    {
        delegate void ItemChangedHandler(IItem item);

        event ItemChangedHandler OnAdded;

        event ItemChangedHandler OnRemoved;

        event IItemStackable.ItemAmountChangedHandler OnStackableChanged;

        void Add(IItem value);

        bool Remove(IItem value);
#if UNITY_EDITOR
        void ClearEditor();
#endif
        HashSet<IItem>.Enumerator GetEnumerator();
    }
}