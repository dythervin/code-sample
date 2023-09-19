using System.Collections.Generic;
using Dythervin.Core;
using Dythervin.Core.Extensions;
using Dythervin.Data.Abstractions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Data.Static
{
    [CreateAssetMenu(menuName = "Containers/Material")]
    public sealed class MaterialContainer : ConstantSoBase<IReadOnlyList<IReadOnlyList<Material>>>
    {
        [SerializeField]
        private Wrapper<Material[]>[] values;

        [SerializeField] private Material[][] _value;

        public Material[] GetRandom() => values.GetRandom().value;

        public Material[] this[int index] => values[index].value;

        public int Count => values.Length;

        public override IReadOnlyList<IReadOnlyList<Material>> Value => _value;

#if UNITY_EDITOR
        [Button]
        private void Set(Material[] materials)
        {
            values = new Wrapper<Material[]>[materials.Length];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = new Wrapper<Material[]>(new[] { materials[i] });
            }

            this.Dirty();
        }
#endif
    }
}