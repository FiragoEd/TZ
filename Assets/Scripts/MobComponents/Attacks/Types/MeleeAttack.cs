using System.Collections;
using PlayerComponents;
using UnityEngine;
using Utils;
using Utils.FadeText;

namespace MobComponents.Attacks.Types
{
    [RequireComponent(typeof(MobMover))]
    [RequireComponent(typeof(Mob))]
    [RequireComponent(typeof(MobAnimator))]
    public class MeleeAttack : MobAttackBase
    {
        [SerializeField] private float AttackCooldown = 2f;
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
                FadeTextSpawner.Instance.SpawnFadedText("-" + mob.Damage, Player.Instance.transform.position);
            }

            mover.Active = true;
            yield return new WaitForSeconds(AttackCooldown);
            attacking = false;
            _attackCoroutine = null;
        }
    }
}