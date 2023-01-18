using System.Collections;
using NTC.Global.Pool;
using ProjectileUtils;
using UnityEngine;

namespace Mob.Attacks.Types
{
    [RequireComponent(typeof(MobMover))]
    [RequireComponent(typeof(Mob))]
    [RequireComponent(typeof(MobAnimator))]
    public class RangeAttack : MobAttackBase
    {
        [SerializeField] private float AttackCooldown = 2f;
        [SerializeField] protected ProjectileBase _bullet;

        protected override IEnumerator Attack()
        {
            mobAnimator.StartAttackAnimation();
            mover.Active = false;
            yield return new WaitForSeconds(AttackDelay);
            var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
            if (playerDistance <= AttackDistance)
            {
                var bullet = NightPool.Spawn(_bullet, transform.position,
                    Quaternion.LookRotation((Player.Instance.transform.position - transform.position).Flat().normalized,
                        Vector3.up));
                bullet.SetDamage(mob.Damage);
            }

            mover.Active = true;
            yield return new WaitForSeconds(AttackCooldown);
            attacking = false;
            _attackCoroutine = null;
        }
    }
}