using UnityEngine;
using Utils.Events;
using Event = Utils.Events.Event;

namespace MobComponents
{
    public class MobAnimator : MonoBehaviour, IMobComponent
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _attackTrigger = "MeleeAttack";

        private readonly InvokableEvent _onAttack = new InvokableEvent();
        public Event OnAttack => _onAttack;

        public void StartAttackAnimation()
        {
            _onAttack?.Invoke();
            _animator.SetTrigger(_attackTrigger);
        }

        public void SetIsRun(bool isRun)
        {
            _animator.SetBool("Run", isRun); // Можно хранить в констанстном виде для детерминированности 
        }

        public void OnSpawn()
        {
            SetIsRun(false);
        }

        public void OnDeath()
        {
            _animator.SetTrigger("Death"); // Можно хранить в констанстном виде для детерминированности 
        }
    }
}