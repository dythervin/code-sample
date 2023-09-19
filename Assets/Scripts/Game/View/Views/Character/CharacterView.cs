using Dythervin.Collections;
using Game.Data;
using UnityEngine;
using Zenject;

namespace Game.View
{
    public class CharacterView : EntityViewExt<ICharacter>
    {
        [SerializeField] private Transform vision;
        [SerializeField] private SerializedDictionary<CharacterType, CharacterMesh> characterMeshes;

        protected override void Init()
        {
            base.Init();
            SetMesh(Model.Data.CharacterType);
        }

        private void SetMesh(CharacterType characterType)
        {
            foreach (var pair in characterMeshes)
            {
                pair.Value.gameObject.SetActive(characterType == pair.Key);
            }
        }

        public override void InstallBindings(DiContainer container)
        {
            base.InstallBindings(container);
            container.BindInstance(vision).WithId(InjectId.TransformVision).AsCached();
        }
    }
}