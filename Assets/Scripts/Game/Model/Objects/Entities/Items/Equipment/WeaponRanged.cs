using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cysharp.Threading.Tasks;
using Dythervin.Core;
using Dythervin.Core.Utils;
using Dythervin.Game.Framework;
using Game.Common;
using Game.Data;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Items
{
    public class WeaponRanged : Weapon<IWeaponRangedData>, IWeaponRanged
    {
        [Inject(Id = InjectId.TransformMuzzle)]
        private Transform _muzzle;

        [Inject]
        private IEntityColliderMap _entityColliderMap;

        IWeaponRangedData IWeaponRanged.Data => Data;

        public event Action<AttackTriggerData> OnTrigger;

        public WeaponRanged(IWeaponRangedData inventoryComponent, IModelContextExt context,
            IModelConstructorContext constructorContext) : base(inventoryComponent, context, constructorContext)
        {
        }

        public void Trigger(in Vector3 target, IReadOnlyList<IWeaponRangedAimAmplifier> aimAmplifiers = null)
        {
            aimAmplifiers ??= Array.Empty<IWeaponRangedAimAmplifier>();
            if (Data.DataAsset.IsRaycast)
            {
                Vector3 muzzlePos = _muzzle.position;
                Vector3 dir = (target - muzzlePos).normalized;
                bool isHit = Physics.Raycast(new Ray(muzzlePos, dir), out RaycastHit hit, Data.DataAsset.Range);

                OnTrigger?.Invoke(new AttackTriggerData(muzzlePos, dir));
                Triggered();

                if (isHit)
                {
                    Color color;
                    if (_entityColliderMap.TryGetValue(hit.colliderInstanceID, out IEntityExt entityTarget))
                    {
                        if (entityTarget != Owner && entityTarget != this)
                        {
                            color = Color.green;
                            entityTarget.GetComponent(out IDamagableComponent damagableComponent);
                            Damage damage = this.GetAttack();
                            damagableComponent.Damage(damage);
                            Hit(new DamageHitData(hit, entityTarget, damage));
                        }
                        else
                        {
                            color = Color.magenta;
                            Owner.LogError(new Exception("Hit self"), true);
                        }
                    }
                    else
                    {
                        color = Color.yellow;
                    }

                    DrawLineContinuous(muzzlePos, hit.point, Cooldown, color);
                }
            }
        }

        [Conditional(Symbols.UNITY_EDITOR)]
        private void DrawLineContinuous(Vector3 muzzlePos, Vector3 point, float duration, Color color)
        {
            DrawLine(muzzlePos, point, duration, color);
        }

        private async UniTask DrawLine(Vector3 muzzlePos, Vector3 point, float duration, Color color)
        {
            for (float sec = 0; sec < duration; sec += Time.deltaTime)
            {
                float t = sec / duration;
                Color tColor = Color.Lerp(color, Color.white, t);
                DDebug.DrawLine(muzzlePos, point, tColor);
                for (int i = 0; i < 3; i++)
                {
                    DDebug.DrawRay(point, Random.insideUnitSphere.normalized * 0.1f, tColor);
                }

                await UniTask.Yield();
            }
        }
    }
}