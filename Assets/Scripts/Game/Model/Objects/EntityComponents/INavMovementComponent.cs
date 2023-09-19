using System;
using Dythervin.Game.Framework;
using Game.AI.PathfindingExt;
using UnityEngine;

namespace Game
{
    public interface INavMovementComponent : IAnyMovementComponent
    {
        event Action OnReached;

        event Action OnMovementStart;

        bool HasReachedDestination { get; }

        bool IsMoving { get; }

        void TurnTo(in Destination target);

        void SetDestination(in Destination destination);

        bool IsInRange(in Destination destination);

        void Stop();
    }

    public interface IAnyMovementComponent : IEntityComponent
    {
        Vector3 Velocity { get; }
    }
}