using System.Collections.Generic;
using System.Runtime.InteropServices;
using Dythervin.Common;
using Dythervin.Game.Framework.Data;
using Dythervin.Serialization.SourceGen;
using UnityEngine;

namespace Game.Data
{
    public enum CharacterType
    {
        None,

        Human,

        Alien01
    }

    [System.Serializable]
    [DSerializable]
    [Guid("04B5EA6B-5F4E-47B6-9B9D-56F8DE4B31D4")]
    public partial class CharacterData : EntityDataExt, ICharacterData
    {
        [DSerializedField(15)]
        [SerializeField] private CharacterType characterType;

        public CharacterData(CharacterType characterType = default, Tag tags = default, IReadOnlyList<IEntityComponentDataExt> components = null) : base(tags, components)
        {
            this.characterType = characterType;
        }

        public CharacterData() : this(default)
        {
        }

        public CharacterType CharacterType => characterType;

        public CharacterData(Tag tags, IEntityComponentDataExt[] components, CharacterType characterType) : base(tags,
            components)
        {
            this.characterType = characterType;
        }
    }

    public interface ICharacterROData : IEntityROData
    {
        CharacterType CharacterType { get; }
    }

    public interface ICharacterData : IEntityDataExt, ICharacterROData
    {
    }
}