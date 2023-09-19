using Dythervin.AutoAttach;
using System.Collections.Generic;
using Dythervin.Game.Framework;
using Dythervin.Game.Framework.View;
using UnityEngine;


namespace Game.View
{
    public class ViewRendererFaction : MonoViewComponentExt<IEntityViewExt>, IViewRenderer
    {
        [Attach(Attach.Child, false)]
        [SerializeField] private Renderer[] renderers;

        public IReadOnlyList<Renderer> Renderers => renderers;

        private IFactionComponent _factionComponent;

        public override bool AllowMultiple => true;

        protected override void Init()
        {
            base.Init();
            Owner.Model.GetComponent(out _factionComponent);
            _factionComponent.OnChanged += UpdateMaterial;
            UpdateMaterial();
        }

        private void UpdateMaterial()
        {
            var mat = _factionComponent.FactionContainer.Get(_factionComponent.Faction);
            foreach (Renderer renderer in renderers)
            {
                renderer.sharedMaterial = mat;
            }
        }
    }
}