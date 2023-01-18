using NTC.Global.Pool;
using PlayerComponents;
using UnityEngine;
using Utils.FadeText;

namespace ProjectileUtils
{
    public class FireBallProjectile : ProjectileBase
    {
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
        }
    }
}