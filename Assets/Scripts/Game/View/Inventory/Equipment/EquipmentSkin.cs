using UnityEngine;

namespace Game.View.Equipment
{
    public abstract class EquipmentSkin : MonoBehaviour
    {
        public bool Visual
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
    }
}