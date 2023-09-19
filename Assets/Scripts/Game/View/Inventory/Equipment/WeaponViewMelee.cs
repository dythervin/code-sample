using Game.Items;

namespace Game.View.Equipment
{
    public class WeaponViewMelee : WeaponView<IWeapon>
    {
        public virtual void Attack(IEntityViewExt target)
        {
        }
    }
}