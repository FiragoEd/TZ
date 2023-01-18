using NTC.Global.Pool;
using UnityEngine;
using Utils.FadeText;

namespace ProjectileUtils
{
    public class BulletProjectile : ProjectileBase
    {
        [SerializeField] private TrailRenderer _trailRenderer;
        protected override void OnTriggerEnter(Collider other)
        {
            if (destroyed)
            {
                return;
            }

            if (DamagePlayer && other.CompareTag("Player"))
            {
                other.GetComponent<Player>().TakeDamage(Damage);
                FadeTextSpawner.Instance.SpawnFadedText("-" + Damage, transform.position);
                destroyed = true;
                NightPool.Despawn(this);
            }

            if (DamageMob && other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<Mob.Mob>();
                mob.TakeDamage(Damage);
                FadeTextSpawner.Instance.SpawnFadedText("-" + Damage, transform.position);
                destroyed = true;
                NightPool.Despawn(this);
            }
        }

        public override void OnDespawn()
        {
            base.OnDespawn();
            _trailRenderer.Clear();
        }
    }
}
