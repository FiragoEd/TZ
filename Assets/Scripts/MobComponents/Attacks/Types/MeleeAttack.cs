using System.Collections;
using UnityEngine;

namespace Mob.Attacks.Types
{
    [RequireComponent(typeof(MobMover))]
    [RequireComponent(typeof(Mob))]
    [RequireComponent(typeof(MobAnimator))]
    public class MeleeAttack : MobAttackBase
    {
        [SerializeField] private float DamageDistance = 1f;
    
        protected override IEnumerator Attack()
        {
            mobAnimator.StartAttackAnimation();
            mover.Active = false;
            yield return new WaitForSeconds(AttackDelay);
            var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
            if (playerDistance <= DamageDistance)
            {
                Player.Instance.TakeDamage(mob.Damage);
            }

            mover.Active = true;
            attacking = false;
            _attackCoroutine = null;
        }
    }
}