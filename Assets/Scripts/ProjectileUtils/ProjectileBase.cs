using NTC.Global.Pool;
using UnityEngine;

namespace ProjectileUtils
{
    public abstract class ProjectileBase : MonoBehaviour, IPoolItem
    {
        [SerializeField] protected float Damage;
        [SerializeField] protected float Speed = 8;
        [SerializeField] protected bool DamagePlayer = false;
        [SerializeField] protected bool DamageMob;
        [SerializeField] protected float TimeToLive = 5f;

        protected float timer = 0f;
        protected bool destroyed = false;

        protected abstract void OnTriggerEnter(Collider other);

        protected virtual void Update()
        {
            if (!destroyed)
            {
                transform.position += transform.forward * (Speed * Time.deltaTime);
            }

            timer += Time.deltaTime;
            if (timer > TimeToLive)
            {
                NightPool.Despawn(gameObject);
            }
        }

        public virtual void OnSpawn()
        {
            timer = 0;
            destroyed = false;
        }

        public virtual void OnDespawn()
        {
        }
        
        public void SetDamage(float damage)
        {
            Damage = damage;
        }
    }
}