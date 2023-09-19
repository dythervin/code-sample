using System;
using Dythervin.Common;
using Dythervin.Core.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.AI.PathfindingExt
{
    [AddComponentMenu("Pathfinding/AI/Movement (3D)")]
    public class Movement : AIPathCustom, IRadiused
    {
        public event DestinationSet OnDestinationSet;

        public event Action OnReached;

        private Destination _destination;

        public ref readonly Destination Destination => ref _destination;

        public float Radius => radius;

        public Vector3 Position => simulatedPosition;

        public void SetDestination(in Destination value)
        {
            _destination = value;
            if (!value.isDynamic)
                destination = value.Point;

            endReachedDistance = ReachDistance(value);
            OnDestinationSet?.Invoke(value);
        }

        public void Stop()
        {
            // if (hasPath || waitingForPathCalculation)
            //     ClearPath();

            destination = simulatedPosition;
        }

        public bool IsInRange(in Destination value)
        {
            return simulatedPosition.InRange(value.Point, value.Radius);
        }

        public bool IsInRange(in Vector3 value, float range)
        {
            return simulatedPosition.InRange(value, range);
        }

        public void SetRandomInCircle(float circleRadius)
        {
            Vector3 offset = Quaternion.Euler(0, Random.Range(0, 360f), 0) * new Vector3(0, 0, circleRadius);
            SetDestination(simulatedPosition + offset);
        }

        public float ReachDistance(in Destination value)
        {
            return radius + value.Radius;
        }

        protected override void OnTargetReached()
        {
            base.OnTargetReached();
            if (_destination.isDynamic && !IsInRange(_destination))
            {
                SearchPath();
                return;
            }

            OnReached?.Invoke();
        }

        public delegate void DestinationSet(in Destination destination);
    }
}