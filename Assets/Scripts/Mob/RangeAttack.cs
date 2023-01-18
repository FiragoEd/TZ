using System.Collections;
using NTC.Global.Pool;
using UnityEngine;

[RequireComponent(typeof(MobMover))]
[RequireComponent(typeof(Mob.Mob))]
public class RangeAttack : MonoBehaviour, IMobComponent
{
    public float AttackDistance = 5f;
    public float AttackDelay = .5f;
    public float AttackCooldown = 2f;
    public Projectile Bullet;

    private MobMover mover;
    private Mob.Mob mob;
    private MobAnimator mobAnimator;
    private bool attacking = false;
    private Coroutine _attackCoroutine = null;

    private void Awake()
    {
        mob = GetComponent<Mob.Mob>();
        mover = GetComponent<MobMover>();
        mobAnimator = GetComponent<MobAnimator>();
        EventBus.Sub(OnDeath, EventBus.PLAYER_DEATH);
    }

    private void OnDestroy()
    {
        EventBus.Unsub(OnDeath, EventBus.PLAYER_DEATH);
    }

    private void Update()
    {
        if (attacking)
        {
            return;
        }

        var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
        if (playerDistance <= AttackDistance)
        {
            attacking = true;
            _attackCoroutine = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        mobAnimator.StartAttackAnimation();
        mover.Active = false;
        yield return new WaitForSeconds(AttackDelay);
        var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
        if (playerDistance <= AttackDistance)
        {
            var bullet = NightPool.Spawn(Bullet, transform.position,
                Quaternion.LookRotation((Player.Instance.transform.position - transform.position).Flat().normalized,
                    Vector3.up));
            bullet.Damage = mob.Damage;
        }

        mover.Active = true;
        yield return new WaitForSeconds(AttackCooldown);
        attacking = false;
        _attackCoroutine = null;
    }

    public void OnSpawn()
    {
        attacking = false;
        _attackCoroutine = null;
        enabled = true;
    }

    public void OnDeath()
    {
        enabled = false;
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
    }
}