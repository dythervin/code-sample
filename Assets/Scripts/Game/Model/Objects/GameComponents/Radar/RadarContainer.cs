using System;
using Dythervin.Collections;
using Dythervin.Common;
using Dythervin.Common.ID;
using Game.Radar;

namespace Game.GameComponents.Radar
{
    public class RadarContainer<T>
        where T : class, IIdentified, IReadOnlyPositioned
    {
        public event Action OnChanged;

        internal readonly LockableDictionary<uint, T> objects = new();
        internal readonly LockableDictionary<uint, IRadar<T>> radars = new();

        public int ObjCount => objects.Count;

        public int RadarCount => radars.Count;

        public void Add(T obj)
        {
            objects.Add(obj.GetId(), obj);
            Changed();
        }

        public void Remove(T obj)
        {
            objects.Remove(obj.GetId());
            Changed();
        }

        public void Add(IRadar<T> obj)
        {
            radars.Add(obj.GetId(), obj);
            Changed();
        }

        public void Remove(IRadar<T> obj)
        {
            radars.Remove(obj.GetId());
            Changed();
        }

        internal void SetLock(bool isLocked)
        {
            objects.SetLock(isLocked);
            radars.SetLock(isLocked);
        }

        private void Changed()
        {
            if (!objects.IsLocked)
                OnChanged?.Invoke();
        }
    }
}