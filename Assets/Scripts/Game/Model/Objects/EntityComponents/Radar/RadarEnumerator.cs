using System;
using System.Collections.Generic;
using Dythervin.Common.ID;
using Game.Data;
using Unity.Collections;

namespace Game.Radar
{
    [Serializable]
    public struct RadarEnumerator<T> : IEnumerator<RadarEnumerator<T>.Data> where T : class, IIdentified
    {
        public readonly struct Data
        {
            public readonly T target;
            public readonly float distance;
            public readonly bool inSightAngle;

            public Data(T target, float distance, bool inSightAngle)
            {
                this.target = target;
                this.distance = distance;
                this.inSightAngle = inSightAngle;
            }
        }


        private NativeList<ObjData> _list;
        private int _index;
        private Data _current;

        internal RadarEnumerator(NativeList<ObjData> list)
        {
            _list = list;
            _index = 0;
            _current = default;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (!_list.IsCreated)
                return false;

            if (_index < _list.Length)
            {
                ObjData data = _list[_index];
                _current = new Data(IdentifiedManager.Instance.Get<T>(data.id), data.distance, data.inVision);
                _index++;
                return true;
            }

            _index = _list.Length + 1;
            _current = default;

            return false;
        }

        public Data Current => _current;

        object System.Collections.IEnumerator.Current
        {
            get
            {
                if (_index == 0 || _index == _list.Length + 1)
                {
                    throw new InvalidOperationException();
                }

                return Current;
            }
        }

        void System.Collections.IEnumerator.Reset()
        {
            _index = 0;
            _current = default;
        }
    }
}