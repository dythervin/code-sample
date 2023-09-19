using Dythervin.AssetIdentifier;
using Dythervin.AssetIdentifier.Addressables;
using Dythervin.Core.Extensions;
using Dythervin.Data.Structs;
using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;
using Game.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.View.Utils
{
    public class PrefabSpawner : MonoBehaviour
    {
        private const string Name = "Spawner";
#if ODIN_INSPECTOR
        [OnValueChanged(nameof(SetName))]
#endif
        [SerializeField] private AssetIdRef<IModelDataAssetWrapper<IEntityROData>> dataAsset;

        [SerializeField] private bool randomRotation;

        [ShowIf(nameof(randomRotation))]
        [SerializeField] private Range<Vector3> rotationRange;

        [Inject] private IAnyFactory _anyFactory;

        [Inject] private IViewMap _viewMap;

        private void Awake()
        {
            Spawn();
#if !UNITY_EDITOR
            Destroy(gameObject);
#endif
        }

        private void Reset()
        {
            SetName();
        }

        private void SetName()
        {
            name = dataAsset.Load() != null ? $"{Name} - {((Object)dataAsset.Load()).name}" : Name;
            this.Dirty();
        }

        [Button]
        [HideInEditorMode]
        private void Spawn()
        {
            IModel obj = _anyFactory.Construct(dataAsset.Load().WrappedData.GetOrCopy());
            IModelView view = _viewMap[obj];

            view.Transform.parent = transform.parent;
            view.Transform.localPosition = transform.localPosition;
            view.Transform.localEulerAngles = randomRotation ? rotationRange.GetRandom() : transform.localEulerAngles;
        }
    }
}