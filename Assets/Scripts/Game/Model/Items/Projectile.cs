using System;
using Dythervin.AutoAttach;
using Dythervin.Game.Framework;
using Game.Common;
using UnityEngine;
using Zenject;


namespace Game.Items
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Projectile : ProjectileBase
    {
        [SerializeField, Attach(Attach.Child)] private Rigidbody rigid;
        [SerializeField, Attach(Attach.Child)] private new Collider collider;

        [Inject] private IEntityColliderMap _colliderMap;

        private Damage _damage;
        private bool _hit;


        public override event Action OnAttack;

        public override event Action OnCollision;

        public override event AttackDelegate OnLaunch;

        public Rigidbody Rigidbody => rigid;

        public Collider Collider => collider;

        private void Awake()
        {
            rigid.useGravity = false;
        }

        public override void Attack(in Vector3 from, in Vector3 target, in Damage damage)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            _damage = damage;

            transform.rotation = Quaternion.LookRotation(target - from);
            _hit = false;
            OnLaunch?.Invoke(damage);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_hit)
                return;

            rigid.useGravity = true;
            _hit = true;
            if (_colliderMap.TryGetValue(collision.collider.GetInstanceID(), out IEntityExt entity) &&
                entity.TryGetComponent(out IDamagable damagable))
            {
                damagable.Damage(_damage);
                OnAttack?.Invoke();
                return;
            }

            OnCollision?.Invoke();
        }

        private void FixedUpdate()
        {
            rigid.velocity = Vector3.forward * _damage.force;
        }
    }
}