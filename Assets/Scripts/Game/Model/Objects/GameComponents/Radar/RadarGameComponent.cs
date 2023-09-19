using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Common;
using Dythervin.Common.ID;
using Dythervin.Common.Utils.JobHandler;
using Dythervin.Game.Framework;
using Dythervin.UnsafeUtils.Unsafe;
using Game.Data;
using Game.Radar;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Game.GameComponents.Radar
{
    public abstract class RadarGameComponent<TData, T> : GameComponentExt<TData>, IRadarGameComponent<T>
        where T : class, IIdentified, IReadOnlyPositioned
        where TData : class, IGameComponentDataExt
    {
        private static readonly Stack<PtrPersistent<NativeList<ObjData>>> Pool = new();

        private static readonly List<NativeList<ObjData>> lists = new List<NativeList<ObjData>>();

        private readonly LockableHashSet<IRadar<T>> _radarContainer = new();

        private bool _isRunning;

        private readonly JobHandler<Job>.CompleteDelegate _action;

        private Job _job;

        private JobHandler<Job> _jobHandle;

        private static readonly Vector3 Vector3Nan = Vector3.negativeInfinity;

        protected abstract IReadOnlyList<T> Targets { get; }

        public RadarGameComponent(TData data, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(data, context, constructorContext)
        {
            _action = Complete;
            _radarContainer.OnChangesApplied += TryStart;
        }

        protected override void LateInit()
        {
            base.LateInit();
            TryStart();
        }

        protected void Run()
        {
            Debug.Assert(!_isRunning && _radarContainer.Count > 0 && Targets.Count > 0);

            _isRunning = true;
            _radarContainer.SetLock(true);
            _job = new Job(new NativeArray<StructT>(Targets.Count, Allocator.TempJob),
                new NativeArray<StructRadar>(_radarContainer.Count, Allocator.TempJob));

            int i = 0;
            foreach (var radar in _radarContainer)
            {
                if (!Pool.TryPop(out var inRadiusList))
                {
                    var list = new NativeList<ObjData>(Allocator.Persistent);
                    lists.Add(list);
                    inRadiusList = new PtrPersistent<NativeList<ObjData>>(ref list);
                }

                StructRadar structRadar = new(radar.GetId(),
                    radar.Position ,
                    radar.Angle,
                    radar.Angle < 360 ? radar.Forward  : float3.zero,
                    inRadiusList,
                    radar.Radius);

                _job.radarArray[i++] = structRadar;
            }

            i = 0;
            foreach (T target in Targets.ToEnumerable())
            {
                StructT structT = new(target.GetId(), target.Position );
                _job.tArray[i++] = structT;
            }

            _jobHandle = _job.Schedule(_radarContainer.Count, 1).OnComplete(_job, _action);
        }

        private void TryStart()
        {
            if (_isRunning)
                return;

            if (_radarContainer.Count > 0 && Targets.Count > 0)
                Run();
        }

        public override void Dispose()
        {
            if (!_jobHandle.IsCompleted)
            {
                _jobHandle.ForceComplete();
            }

            foreach (var ptrPersistent in Pool)
            {
                ptrPersistent.Dispose();
            }

            Pool.Clear();
            foreach (var list in lists)
            {
                if (list.IsCreated)
                    list.Dispose();
            }

            lists.Clear();

            base.Dispose();
        }

        private void Complete(in Job value, float delta)
        {
            for (int i = 0; i < value.radarArray.Length; i++)
            {
                StructRadar radarStruct = value.radarArray[i];
                IdentifiedManager.Instance.TryGet(radarStruct.id, out IRadar<T> radar);
                if (radar == null)
                    continue;

                if (radar.InRadius.IsValid)
                {
                    radar.InRadius.Value.Clear();
                    Pool.Push(radar.InRadius);
                }

                radar.InRadius = radarStruct.inRadius;
                radar.RefreshCallback();
            }

            _radarContainer.SetLock(false);
            _job.radarArray.Dispose();
            _job.tArray.Dispose();
            _isRunning = false;
            TryStart();
        }

        public struct StructT
        {
            public readonly uint id;

            public readonly float3 position;

            public StructT(uint id, in float3 position)
            {
                this.position = position;
                this.id = id;
            }
        }

        public struct StructRadar
        {
            public readonly uint id;

            public readonly float3 position;

            public readonly float angle;

            public readonly float radius;

            public readonly float3 forward;

            public PtrPersistent<NativeList<ObjData>> inRadius;

            public StructRadar(uint id, float3 position, float angle, float3 forward,
                PtrPersistent<NativeList<ObjData>> inRadius, float radius)
            {
                this.id = id;
                this.position = position;
                this.angle = angle;
                this.forward = forward;
                this.inRadius = inRadius;
                this.radius = radius;
            }
        }

        public struct Comparer : IComparer<ObjData>
        {
            public int Compare(ObjData x, ObjData y)
            {
                return x.distance.CompareTo(y.distance);
            }
        }

        [BurstCompile]
        public struct Job : IJobParallelFor
        {
            [ReadOnly]
            public NativeArray<StructT> tArray;

            [ReadOnly]
            public NativeArray<StructRadar> radarArray;

            private static readonly Comparer Comparer = new Comparer();

            public Job(NativeArray<StructT> tArray, NativeArray<StructRadar> radarArray)
            {
                this.tArray = tArray;
                this.radarArray = radarArray;
            }

            public void Execute(int index)
            {
                StructRadar radar = radarArray[index];
                float dot = math.clamp((radar.angle - 180) / -180, -1, 1);
                for (int i = 0; i < tArray.Length; i++)
                {
                    StructT target = tArray[i];
                    float3 diff = target.position - radar.position;
                    float distance = math.length(diff);
                    if (distance > radar.radius)
                        continue;

                    radar.inRadius.Value.Add(new ObjData(target.id,
                        distance,
                        radar.angle >= 360 || math.dot(diff / distance, radar.forward) >= dot));
                }

                radar.inRadius.Value.Sort(Comparer);
            }
        }

        public void Add(IRadar<T> radar)
        {
            _radarContainer.Add(radar);
            TryStart();
        }

        public void Remove(IRadar<T> radar)
        {
            _radarContainer.Remove(radar);
        }
    }
}