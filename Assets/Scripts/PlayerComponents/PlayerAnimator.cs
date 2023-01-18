using UnityEngine;

namespace PlayerComponents
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            EventBus<PlayerInputMessage>.Sub(AnimateRun);
            EventBus.Sub(AnimateDeath, EventBus.PLAYER_DEATH);
        }

        private void OnDestroy()
        {
            EventBus.Unsub(AnimateDeath, EventBus.PLAYER_DEATH);
            EventBus<PlayerInputMessage>.Unsub(AnimateRun);
        }

        private void AnimateRun(PlayerInputMessage message)
        {
            _animator.SetBool("IsRun", message.MovementDirection.sqrMagnitude > 0);
        }

        private void AnimateDeath()
        {
            _animator.SetTrigger("Death");
        }

        public void TriggerShoot()
        {
            _animator.SetTrigger("Shoot");
        }
    }
}