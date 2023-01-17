using UnityEngine;
using Utils;
using Utils.Events;
using Event = Utils.Events.Event;

namespace Mob
{
    public class Mob : MonoBehaviour
    {
        public float Damage = 1;
        public float MoveSpeed = 3.5f;
        public float Health = 3;
        public float MaxHealth = 3;

        
        #region Events

        private readonly InvokableEvent<DeltaHP> _onHPChange = new InvokableEvent<DeltaHP>();
        public Event<DeltaHP> OnHPChange => _onHPChange;
        
        private readonly InvokableEvent _onDeath = new InvokableEvent();
        public Event OnDeath => _onDeath;

        #endregion
        

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
        }
    }
}