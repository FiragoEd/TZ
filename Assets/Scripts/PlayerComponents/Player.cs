using PowerUps;
using PowerUps.Behaviour;
using UnityEngine;
using Utils;
using Utils.Events;
using Event = Utils.Events.Event;

namespace PlayerComponents
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        [SerializeField] private float _damage = 1;
        [SerializeField] private float _moveSpeed = 3.5f;
        [SerializeField] private float _health = 3;
        [SerializeField] private float _maxHealth = 3;

        public float Damage => _damage;
        public float MoveSpeed => _moveSpeed;
        public float Health => _health;
        public float MaxHealth => _maxHealth;

        #region Events

        private readonly InvokableEvent<WeaponType> _onWeaponChange = new InvokableEvent<WeaponType>();
        public Event<WeaponType> OnWeaponChange => _onWeaponChange;

        private readonly InvokableEvent<DeltaHP> _onHPChange = new InvokableEvent<DeltaHP>();
        public Event<DeltaHP> OnHPChange => _onHPChange;

        private readonly InvokableEvent _onUpgrade = new InvokableEvent();
        public Event OnUpgrade => _onUpgrade;

        #endregion


        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void TakeDamage(float amount)
        {
            if (_health <= 0)
                return;
            _health -= amount;
            if (_health <= 0)
            {
                EventBus.Pub(EventBus.PLAYER_DEATH);
            }

            _onHPChange?.Invoke(new DeltaHP()
            {
                currentHP = _health,
                deltaHP = -amount
            });
        }

        public void Heal(float amount)
        {
            if (_health <= 0)
                return;
            _health += amount;
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }

            _onHPChange?.Invoke(new DeltaHP()
            {
                currentHP = _health,
                deltaHP = amount
            });
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PowerUpComponentBase>())
                other.GetComponent<PowerUpComponentBase>().ApplyPowerUp();
        }

        public void Upgrade(float hp, float dmg, float ms)
        {
            _damage += dmg;
            _health += hp;
            _maxHealth += hp;
            _moveSpeed += ms;
            _onUpgrade?.Invoke();
            _onHPChange?.Invoke(new DeltaHP()
            {
                currentHP = _health,
                deltaHP = 0
            });
        }

        public void ChangeWeapon(WeaponType type)
        {
            _onWeaponChange?.Invoke(type);
        }
    }
}