using System;
using Dythervin.Common;
using Dythervin.Common.ID;
using Dythervin.UnsafeUtils.Unsafe;
using Game.Data;
using Unity.Collections;
using UnityEngine;

namespace Game.Radar
{
    public interface IRadar<T> : IIdentified where T : class, IIdentified, IReadOnlyPositioned
    {
        float Angle { get; }

        float Radius { get; }

        protected internal PtrPersistent<NativeList<ObjData>> InRadius { get; set; }

        Vector3 Forward { get; }

        Vector3 Position { get; }

        void RefreshCallback();

        event Action OnRefresh;

        RadarEnumerator<T> GetEnumerator();
    }
}