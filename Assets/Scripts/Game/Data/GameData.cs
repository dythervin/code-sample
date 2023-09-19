using System;
using System.Collections.Generic;
using Dythervin.Common;
using Dythervin.Game.Framework.Data;
using UnityEngine;

namespace Game.Data
{
    [System.Serializable]
    public class GameData : ModelData, IGameData
    {
        [SerializeReference] private IGameComponentData[] components;

        protected override TypeID<IModelData> GroupId => Features.GetDataGroupId<IGameData>();

        public IReadOnlyList<IGameComponentData> Components => components;

        public GameData(params IGameComponentData[] components) : base()
        {
            this.components = components;
        }
        public override bool IsReadOnly => false;
        public GameData()
        {
            components = Array.Empty<IGameComponentData>();
        }

        IReadOnlyList<IModelComponentData> IModelComponentOwnerROData.Components => Components;
    }
}