using System;
using System.Runtime.InteropServices;
using Dythervin.Collections;
using Dythervin.Serialization;
using Dythervin.Serialization.SourceGen;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Data
{
    [Guid("8247246A-5067-49B6-8750-30AB1C586517")]
    [DSerializable]
    [Serializable]
    [HideLabel]
    public partial class SerializableHashList<T> : ISerializationCallbackReceiver
        where T : class, IDSerializable
    {
        [SerializeReference] public T[] array;

        [DSerializedField(0)]
        public HashList<T> hashList;

        public virtual ushort Version => 0;

        public SerializableHashList()
        {
        }

        public SerializableHashList(HashList<T> hashList)
        {
            this.hashList = hashList;
        }

        protected virtual void OnBeforeUnitySerialize()
        {
        }

        protected virtual void OnAfterUnityDeserialize()
        {
            if (array == null)
            {
                return;
            }

            hashList ??= new HashList<T>();
            hashList.Clear();
            foreach (T t in array)
            {
                hashList.Add(t);
            }

#if !UNITY_EDITOR
            Dythervin.ObjectPool.ArrayPools.Release(ref array);
#endif
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            OnBeforeUnitySerialize();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            OnAfterUnityDeserialize();
        }
    }
}