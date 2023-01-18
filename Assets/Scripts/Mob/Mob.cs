using DG.Tweening;
using NTC.Global.Pool;
using UnityEngine;
using Utils;
using Utils.Events;
using Event = Utils.Events.Event;

namespace Mob
{
    public class Mob : MonoBehaviour, IPoolItem
    {
        private const float TIME_TO_DESPAWN = 2;
        private const float DELTA_Y = 2;
        public float Damage = 1;
        public float MoveSpeed = 3.5f;
        public float Health = 3;
        public float MaxHealth = 3;


        private float _yPos;

        #region Events

        private readonly InvokableEvent<DeltaHP> _onHPChange = new InvokableEvent<DeltaHP>();
        public Event<DeltaHP> OnHPChange => _onHPChange;

        private readonly InvokableEvent _onDeath = new InvokableEvent();
        public Event OnDeath => _onDeath;
        
        private readonly InvokableEvent _onSpawnEvent = new InvokableEvent();
        public Event OnSpawnEvent => _onSpawnEvent;

        #endregion

        private void Start()
        {
            _yPos = transform.position.y;
        }

        public void OnSpawn()
        {
            Health = MaxHealth;
            _onSpawnEvent?.Invoke();
            var components = GetComponents<IMobComponent>();
            foreach (var component in components)
            {
                component.OnSpawn();
            }
            GetComponent<Collider>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }

        public void OnDespawn()
        {
            transform.position += new Vector3(0, _yPos, 0);
        }

        public void TakeDamage(float amount)
        {
            if (Health <= 0)
                return;
            Health -= amount;
            _onHPChange?.Invoke(new DeltaHP()
            {
                currentHP = Health,
                deltaHP = -amount
            });
            
            if (Health <= 0)
            {
                Death();
            }
        }

        public void Heal(float amount)
        {
            if (Health <= 0)
                return;
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            _onHPChange?.Invoke(new DeltaHP()
            {
                currentHP = Health,
                deltaHP = amount
            });
        }

        private void Death()
        {
            EventBus.Pub(EventBus.MOB_KILLED);
            var components = GetComponents<IMobComponent>();
            _onDeath?.Invoke();
            foreach (var component in components)
            {
                component.OnDeath();
            }

            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.DOMoveY(-DELTA_Y, TIME_TO_DESPAWN * 1 / 4f).SetDelay(TIME_TO_DESPAWN * 3 / 4f);
            NightPool.Despawn(this, TIME_TO_DESPAWN);
        }
    }
}