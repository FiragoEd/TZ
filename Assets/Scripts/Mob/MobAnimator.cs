using UnityEngine;
using Utils.Events;
using Event = Utils.Events.Event;

public class MobAnimator : MonoBehaviour, IMobComponent
{
    public Animator Animator;
    public string AttackTrigger = "MeleeAttack";

    private readonly InvokableEvent _onAttack = new InvokableEvent();
    public Event OnAttack => _onAttack;
    
    public void StartAttackAnimation()
    {
        _onAttack?.Invoke();
        Animator.SetTrigger(AttackTrigger);
    }

    public void SetIsRun(bool isRun)
    {
        Animator.SetBool("Run", isRun);
    }

    public void OnDeath()
    {
        Animator.SetTrigger("Death");
    }
}