using DG.Tweening;
using NTC.Global.Pool;
using UnityEngine;
using Utils;
using Utils.Events;
using Event = Utils.Events.Event;

namespace MobComponents
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(IMobComponent))]
    public class Mob : MonoBehaviour, IPoolItem
    {
        private const float TIME_TO_DESPAWN = 2;
        private const float DELTA_Y = 2;

        [SerializeField] private float _damage = 1;
        [SerializeField] private float _moveSpeed = 3.5f;
        [SerializeField] private float _health = 3;
        [SerializeField] private float _maxHealth = 3;

        private float _yPos;
        private IMobComponent[] _cachedComponents;
        private Collider _cachedCollider;
        private Rigidbody _cachedRigidbody;

        #region Events

        private readonly InvokableEvent<DeltaHP> _onHPChange = new InvokableEvent<DeltaHP>();
        public Event<DeltaHP> OnHPChange => _onHPChange;

        private readonly InvokableEvent _onDeath = new InvokableEvent();
        public Event OnDeath => _onDeath;

        private readonly InvokableEvent _onSpawnEvent = new InvokableEvent();
        public Event OnSpawnEvent => _onSpawnEvent;

        #endregion

        public float Damage => _damage;
        public float MoveSpeed => _moveSpeed;
        public float Health => _health;
        public float MaxHealth => _maxHealth;

        private void Awake()
        {
            _cachedComponents = GetComponents<IMobComponent>();
            _cachedCollider = GetComponent<Collider>();
            _cachedRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _yPos = transform.position.y;
        }

        public void OnSpawn()
        {
            _health = _maxHealth;
            _onSpawnEvent?.Invoke();
            foreach (var component in _cachedComponents)
            {
                component.OnSpawn();
            }

            _cachedCollider.enabled = true;
            _cachedRigidbody.isKinematic = false;
        }

        public void OnDespawn()
        {
            transform.position += new Vector3(0, _yPos, 0);
        }

        public void TakeDamage(float amount)
        {
            if (_health <= 0)
                return;
            _health -= amount;
            _onHPChange?.Invoke(new DeltaHP()
            {
                currentHP = _health,
                deltaHP = -amount
            });

            if (_health <= 0)
            {
                Death();
            }
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

        private void Death()
        {
            EventBus.Pub(EventBus.MOB_KILLED);
            _onDeath?.Invoke();
            foreach (var component in _cachedComponents)
            {
                component.OnDeath();
            }

            _cachedCollider.enabled = false;
            _cachedRigidbody.isKinematic = true;
            transform.DOMoveY(-DELTA_Y, TIME_TO_DESPAWN * 1 / 4f).SetDelay(TIME_TO_DESPAWN * 3 / 4f);
            NightPool.Despawn(this, TIME_TO_DESPAWN);
        }
    }
}