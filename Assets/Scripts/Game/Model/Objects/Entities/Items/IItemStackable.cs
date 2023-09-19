namespace Game.Items
{
    public interface IItemStackable : IItem
    {
        public delegate void ItemAmountChangedHandler(IItemStackable item, int change);

        event ItemAmountChangedHandler ItemAmountChanged;

        int Amount { get; set; }
    }
}