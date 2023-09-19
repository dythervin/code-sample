using System;
using Dythervin.Common;
using Dythervin.Core.Extensions;
using Pathfinding;
using UnityEngine;

namespace Game.AI.PathfindingExt
{
    /// <summary>
    ///     Sets the destination of an AI to the position of a specified object.
    ///     This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
    ///     This component will then make the AI move towards the <see cref="_target" /> set on this component.
    ///     See: <see cref="Pathfinding.IAstarAI.destination" />
    ///     [Open online documentation to see images]
    /// </summary>
    [UniqueComponent(tag = "ai.destination")]
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
    public class MovementDynamicTarget : MonoBehaviour
    {
        [SerializeField] private Movement movement;

        /// <summary>The object that the AI should move to</summary>
        private IRadiused _target;

        private Action _fetchTargetDestination;

        private float _repathInterval;
        private float _maxInterval;

        protected void Awake()
        {
            movement.OnDestinationSet += OnDestinationSet;
            movement.OnReached += OnReached;
            _fetchTargetDestination = () =>
            {
                if (enabled)
                    movement.destination = _target.Position;
            };
            enabled = _target != null;
            movement.onSearchPath += _fetchTargetDestination;
        }

        private void OnDestroy()
        {
            movement.onSearchPath -= _fetchTargetDestination;
        }

        private void Reset()
        {
            TryGetComponent(out movement);
        }

        private void OnEnable()
        {
            AutoRepathPolicy repath = movement.autoRepath;
            _repathInterval = repath.interval;
            _maxInterval = repath.maximumInterval;

            repath.interval = 0.1f;
            repath.maximumInterval = 0.5f;

            _fetchTargetDestination();
            movement.SearchPath();
        }

        private void OnDisable()
        {
            movement.autoRepath.interval = _repathInterval;
            movement.autoRepath.maximumInterval = _maxInterval;

            movement.ResetStoppingDistance();
        }

        private void OnReached()
        {
            //enabled = movement.Destination.pursue;
            enabled = false;
        }

        private void OnDestinationSet(in Destination destination)
        {
            _target = destination.targetDynamic;
            enabled = destination.isDynamic;
        }
    }
}