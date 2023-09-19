namespace Game.View.Equipment
{
    public abstract class EquipmentView<TObserver> : EntityViewExt<TObserver>
        where TObserver : class, IEntityExt
    {
        public bool Visual
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public virtual void Throw()
        {
        }
    }
}