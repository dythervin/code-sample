using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Dythervin.Common;
using Dythervin.Game.Framework.Data;
using Game.Data;

namespace Game.View
{
    [System.Serializable]
    [Guid("EAF8A981-317C-4FCF-BDE4-54EBCEABF8D5")]
    [Dythervin.Serialization.SourceGen.DSerializable]
    public class WrappedEntityData<TEntityDataExt> : WrappedModelData<TEntityDataExt>, IEntityDataExt
        where TEntityDataExt : class, IEntityDataExt
    {
        public Tag Tags
        {
            get => WrappedData.Tags;
            set => throw new NotSupportedException();
        }

        public IReadOnlyList<IEntityComponentData> Components => WrappedData.Components;

        IReadOnlyList<IModelComponentData> IModelComponentOwnerROData.Components =>
            ((IModelComponentOwnerROData)WrappedData).Components;

        public float Radius => WrappedData.Radius;

        public IEntityVars VarResolver => WrappedData.VarResolver;

        IEntityROVars IEntityRODataExt.VarResolver => ((IEntityRODataExt)WrappedData).VarResolver;
    }
}