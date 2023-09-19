using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Game.Framework.Data;
using Dythervin.Serialization.SourceGen;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    [DSerializable]
    public abstract partial class ModelComponentOwnerData<TComponent> : ModelData,
        IModelComponentOwnerData
        where TComponent : class, IModelComponentData
    {
        [SerializeField]
        [DSerializedField(5)]
        private SerializableHashList<TComponent> components;

        public HashList<TComponent> Components => components.hashList;

        protected ModelComponentOwnerData([CanBeNull] IReadOnlyList<TComponent> components)
        {
            this.components = new SerializableHashList<TComponent>(components != null ?
                new HashList<TComponent>(components) :
                new HashList<TComponent>());
        }

        private ModelComponentOwnerData()
        {
        }

        IReadOnlyList<IModelComponentData> IModelComponentOwnerROData.Components => Components;
    }
}