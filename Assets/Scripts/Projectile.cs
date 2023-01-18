using NTC.Global.Pool;
using UnityEngine;
using Utils.FadeText;

public class Projectile : MonoBehaviour, IPoolItem
{
    [SerializeField] private TrailRenderer _trailRenderer;
    public float Damage;
    public float Speed = 8;
    public bool DamagePlayer = false;
    public bool DamageMob;
    public float TimeToLive = 5f;
    private float timer = 0f;
    private bool destroyed = false;

    protected virtual void OnTriggerEnter(Collider other)
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
        }

        if (DamageMob && other.CompareTag("Mob"))
        {
            var mob = other.GetComponent<Mob.Mob>();
            mob.TakeDamage(Damage);
            FadeTextSpawner.Instance.SpawnFadedText("-" + Damage, transform.position);
            destroyed = true;
        }
    }

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

    public void OnSpawn()
    {
        timer = 0;
        destroyed = false;
    }

    public void OnDespawn()
    {
        _trailRenderer.Clear();
    }
}