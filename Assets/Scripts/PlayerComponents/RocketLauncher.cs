using System.Threading.Tasks;
using NTC.Global.Pool;
using PowerUps;
using ProjectileUtils;
using UnityEngine;

namespace PlayerComponents
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(PlayerAnimator))]
    public sealed class RocketLauncher : PlayerWeapon
    {
        protected override WeaponType _type => WeaponType.RocketLauncher;
        [SerializeField] private BulletProjectile _bulletPrefab;
        [SerializeField] private float _reload = 1f;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private ParticleSystem _vfxEffect;

        private float _lastTime;

        private PlayerAnimator _playerAnimator;

        protected override void Awake()
        {
            base.Awake();
            _playerAnimator = GetComponent<PlayerAnimator>();
        }

        private void Start()
        {
            _lastTime = Time.time - _reload;
        }

        private float GetDamage()
        {
            return _player.Damage * 2f;
        }

        protected override async void Fire(PlayerInputMessage message)
        {
            if (Time.time - _reload < _lastTime)
            {
                return;
            }

            if (!message.Fire)
            {
                return;
            }

            _lastTime = Time.time;
            _playerAnimator.TriggerShoot();

            await Task.Delay(16);

            var bullet = NightPool.Spawn(_bulletPrefab, _firePoint.position, transform.rotation);
            bullet.SetDamage(GetDamage());
            _vfxEffect.Play();
        }
    }
}