using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Dythervin.Common;
using Dythervin.Game.Framework.Data;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    [DSerializable]
    [Guid("E8A79915-6BA3-4DAA-9695-98A2C94770AE")]
    public abstract partial class EntityDataExt : ModelComponentOwnerData<IEntityComponentDataExt>, IEntityDataExt
    {
        [DSerializedField(10)]
        [SerializeField] private Tag tags;

        [DSerializedField(11)]
        [SerializeField]
        private float radius = 0.5f;

        public override bool IsReadOnly => false;

        public float Radius => radius;

        public IEntityVars VarResolver { get; } = new EntityVars();

        public Tag Tags
        {
            get => tags;
            set => tags = value;
        }

        protected override TypeID<IModelData> GroupId => Features.GetDataGroupId<IEntityROData>();

        IEntityROVars IEntityRODataExt.VarResolver => VarResolver;

        IReadOnlyList<IEntityComponentData> IEntityROData.Components => Components;

        IReadOnlyList<IModelComponentData> IModelComponentOwnerROData.Components => Components;

        Tag IEntityROData.Tags => tags;

        protected EntityDataExt(Tag tags = default, IReadOnlyList<IEntityComponentDataExt> components = null) :
            base(components)
        {
            this.tags = tags;
        }
    }
}