using System;
using Dythervin.AssetIdentifier;
using Dythervin.AssetIdentifier.Addressables;
using Dythervin.Common;
using Sirenix.OdinInspector;
using Object = UnityEngine.Object;

namespace Game.Data
{
    public static class ItemDatabaseEditorHelper
    {
        private static readonly ValueDropdownList<AssetId> Buffer = new ValueDropdownList<AssetId>();

        public static ValueDropdownList<AssetId> Get<T>(Predicate<T> predicate = null)
            where T : class, IItemDataAsset
        {
            predicate ??= Predicates<T>.True;
            Buffer.Clear();
            Buffer.Add("None", default);
#if UNITY_EDITOR
            foreach (var item in AssetIdentifiedDatabase.Instance.GetReferencesEditor<T>())
            {
                Object obj = item.editorAsset;
                if (obj is T t && predicate.Invoke(t))
                {
                    Buffer.Add(obj.name, t.Id);
                }
            }
#endif
            return Buffer;
        }
    }
}