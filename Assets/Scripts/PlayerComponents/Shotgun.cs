using System.Threading.Tasks;
using NTC.Global.Pool;
using PowerUps;
using ProjectileUtils;
using UnityEngine;

namespace PlayerComponents
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(PlayerAnimator))]
    public sealed class Shotgun : PlayerWeapon
    {
        protected override WeaponType _type => WeaponType.Shotgun;
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
            return _player.Damage;
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
            var directions = SpreadDirections(transform.rotation.eulerAngles, 3, 20);
            foreach (var direction in directions)
            {
                var bullet = NightPool.Spawn(_bulletPrefab, _firePoint.position, Quaternion.Euler(direction));
                bullet.SetDamage(GetDamage());
            }

            _vfxEffect.Play();
        }

        private Vector3[] SpreadDirections(Vector3 direction, int num, int spreadAngle)
        {
            Vector3[] result = new Vector3[num];
            result[0] = new Vector3(0, direction.y - (num - 1) * spreadAngle / 2, 0);
            for (int i = 1; i < num; i++)
            {
                result[i] = result[i - 1] + new Vector3(0, spreadAngle, 0);
            }

            return result;
        }
    }
}