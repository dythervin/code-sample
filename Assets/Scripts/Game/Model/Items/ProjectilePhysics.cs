using System;
using Dythervin.Game.Framework;
using Game.Common;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using Zenject;


namespace Game.Items
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ProjectilePhysics : ProjectileBase
    {
        [SerializeField] private Rigidbody rigid;
        [SerializeField] private new Collider collider;

        [Inject] private IEntityColliderMap _colliderMap;

        private Damage _damage;
        private bool _hit;


        public override event Action OnAttack;

        public override event Action OnCollision;

        public override event AttackDelegate OnLaunch;

        public Rigidbody Rigidbody => rigid;

        public Collider Collider => collider;

        public override void Attack(in Vector3 from, in Vector3 target, in Damage damage)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            _damage = damage;


            float angle = CalculateAngle(damage.force, rigid.mass, from, target, false);
            transform.localEulerAngles = new Vector3(360f - angle, 0f, 0f);
            rigid.AddRelativeForce(0, 0, damage.force, ForceMode.VelocityChange);
            _hit = false;
            OnLaunch?.Invoke(damage);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_hit)
                return;

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

        [BurstCompile]
        private static float CalculateAngle(float force, float mass, float3 source, float3 target, bool low)
        {
            float3 targetDir = target - source;
            float y = targetDir.y;
            targetDir.y = 0f;
            float x = math.length(targetDir);
            float gravity = mass * -Physics.gravity.y;
            float sSqr = force * force;
            float underTheSqrRoot = sSqr * sSqr - gravity * (gravity * x * x + 2 * y * sSqr);

            float root = math.sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            return math.degrees(math.atan2(low ? lowAngle : highAngle, gravity * x));
        }
    }
}