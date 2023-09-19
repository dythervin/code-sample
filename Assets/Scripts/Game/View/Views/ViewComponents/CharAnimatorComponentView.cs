using System;
using Dythervin.AI.GOAP;
using Dythervin.AutoAttach;
using Dythervin.Game.Framework;
using Dythervin.UpdateSystem;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.View.ViewComponents
{
    public class CharAnimatorComponentView : MonoViewComponentExt<CharacterView>, IUpdatable, ICharAnimatorComponentView
    {
        private static readonly int IdleEnter = Animator.StringToHash("IdleEnter");
        private static readonly int IdleId = Animator.StringToHash("IdleId");
        private static readonly int Idling = Animator.StringToHash("Idling");
        private static readonly int IdleExit = Animator.StringToHash("IdleExit");
        private static readonly int Forward = Animator.StringToHash("Forward");
        private static readonly int Right = Animator.StringToHash("Right");
        private static readonly int Level = Animator.StringToHash("SuspiciousLevel");
        [SerializeField] private float idleAnimNum = 1;

        [Attach(Attach.ZenjectContext)]
        [SerializeField] private Rigidbody[] rigidbodies;

        [Inject] private Animator _animator;
        [Inject] private Transform _root;
        private ISuspiciousLevelComponent _suspiciousLevelComponent;

        private IHealthComponent _health;
        private IAnyMovementComponent _movement;

        protected override void Init()
        {
            base.Init();
            SetRagdoll(false);
            Owner.Model.GetComponent(out _health);
            Owner.Model.GetComponent(out _movement);
            Owner.Model.GetComponent(out _suspiciousLevelComponent);
            _health.OnDeath += DeathCallback;
        }

        protected override void LateInit()
        {
            base.LateInit();
            _suspiciousLevelComponent.OnChanged += SuspiciousLevelComponent_OnChanged;
            this.SetUpdater(true);
        }

        protected override void Destroyed()
        {
            base.Destroyed();
            this.SetUpdater(false);
        }

        private void DeathCallback()
        {
            SetRagdoll(true);
        }

        public void SetRagdoll(bool value)
        {
            _animator.enabled = !value;
            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = !value;
            }
        }

        public void TriggerRandomIdle()
        {
            _animator.SetFloat(IdleId, Random.Range(0, idleAnimNum));
            _animator.SetTrigger(IdleEnter);
        }

        public void ExitIdle()
        {
            AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);
            if (state.shortNameHash == Idling)
            {
                _animator.SetTrigger(IdleExit);
            }
        }

        void IUpdatable.OnUpdate()
        {
            Vector3 direction = _root.InverseTransformDirection(_movement.Velocity);
            _animator.SetFloat(Forward, direction.z);
            _animator.SetFloat(Right, direction.x);
        }

        private void SuspiciousLevelComponent_OnChanged(SuspiciousLevel obj)
        {
            int level = obj switch
            {
                SuspiciousLevel.Alert => 2,
                SuspiciousLevel.Suspicious => 1,
                SuspiciousLevel.Calm => 0,
                _ => throw new ArgumentOutOfRangeException(nameof(obj), obj, null)
            };

            _animator.SetInteger(Level, level);
        }
    }
}