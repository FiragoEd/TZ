using System.Collections;
using UnityEngine;

namespace Mob.Attacks
{
    [RequireComponent(typeof(MobMover))]
    [RequireComponent(typeof(Mob))]
    [RequireComponent(typeof(MobAnimator))]
    public abstract class MobAttackBase : MonoBehaviour, IMobComponent
    {
        [SerializeField] protected float AttackDistance = 5f;
        [SerializeField] protected float AttackDelay = .5f;

        protected MobMover mover;
        protected Mob mob;
        protected MobAnimator mobAnimator;
        protected bool attacking = false;
        protected Coroutine _attackCoroutine = null;
        
        
        private void Awake()
        {
            mob = GetComponent<Mob>();
            mover = GetComponent<MobMover>();
            mobAnimator = GetComponent<MobAnimator>();
            EventBus.Sub(OnDeath,EventBus.PLAYER_DEATH);
        }

        private void OnDestroy()
        {
            EventBus.Unsub(OnDeath,EventBus.PLAYER_DEATH);
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
        
        
        protected abstract IEnumerator Attack();
        
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
}