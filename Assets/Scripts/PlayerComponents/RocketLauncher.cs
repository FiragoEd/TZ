using System.Threading.Tasks;
using NTC.Global.Pool;
using PowerUps;
using ProjectileUtils;
using UnityEngine;

namespace PlayerComponents
{
    public class RocketLauncher : PlayerWeapon
    {
        public override WeaponType Type => WeaponType.RocketLauncher;
        public BulletProjectile BulletPrefab;
        public float Reload = 1f;
        public Transform FirePoint;
        public ParticleSystem VFX;

        protected float lastTime;

        protected override void Awake()
        {
            base.Awake();
            lastTime = Time.time - Reload;
        }

        protected virtual float GetDamage()
        {
            return GetComponent<Player>().Damage * 2f;
        }

        protected override async void Fire(PlayerInputMessage message)
        {
            if (Time.time - Reload < lastTime)
            {
                return;
            }

            if (!message.Fire)
            {
                return;
            }

            lastTime = Time.time;
            GetComponent<PlayerAnimator>().TriggerShoot();

            await Task.Delay(16);

            var bullet = NightPool.Spawn(BulletPrefab, FirePoint.position, transform.rotation);
            bullet.SetDamage(GetDamage()); // TODO : кэшировать
            VFX.Play();
        }
    }
}