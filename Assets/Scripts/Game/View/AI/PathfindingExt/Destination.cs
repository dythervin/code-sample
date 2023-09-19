using System.Runtime.InteropServices;
using Dythervin.Common;
using Dythervin.Core.Extensions;
using Unity.Mathematics;
using UnityEngine;

namespace Game.AI.PathfindingExt
{
    [StructLayout(LayoutKind.Auto)]
    public struct Destination
    {
        public readonly bool isDynamic;

        public readonly bool pursue;

        public readonly IRadiused targetDynamic;

        private readonly Vector3 _target;

        public float _reachDistance;

        public float Radius => isDynamic ? targetDynamic.Radius + _reachDistance : _reachDistance;

        public float RadiusSqr => math.pow(Radius, 2);

        public Vector3 Point => isDynamic ? targetDynamic.Position : _target;

        public Destination(IRadiused targetDynamic, bool pursue, float reachDistance = 0.05f)
        {
            isDynamic = true;
            this.targetDynamic = targetDynamic;
            _target = default;
            this.pursue = pursue;
            _reachDistance = reachDistance;
        }

        public Destination(in Vector3 target, float reachDistance = 0)
        {
            isDynamic = false;
            targetDynamic = null;
            _target = target;
            pursue = false;
            _reachDistance = reachDistance;
        }

        public static implicit operator Destination(in Vector3 destination)
        {
            return new Destination(destination);
        }
    }
}