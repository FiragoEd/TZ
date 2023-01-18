using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator Animator;

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
        Animator.SetBool("IsRun", message.MovementDirection.sqrMagnitude > 0);
    }

    private void AnimateDeath()
    {
        Animator.SetTrigger("Death");
    }

    public void TriggerShoot()
    {
        Animator.SetTrigger("Shoot");
    }
}