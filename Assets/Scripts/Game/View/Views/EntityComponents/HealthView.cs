using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    [DisallowMultipleComponent]
    public sealed class HealthView : EntityComponentView<HealthComponent>
    {
        [SerializeField] private Image image;

        protected override void Init()
        {
            base.Init();
            Model.OnChanged += ModelOnOnChanged;
        }

        protected override void Destroyed()
        {
            Model.OnChanged -= ModelOnOnChanged;
            base.Destroyed();
        }

        private void ModelOnOnChanged()
        {
            image.fillAmount = Model.GetValuePrc();
        }
    }
}