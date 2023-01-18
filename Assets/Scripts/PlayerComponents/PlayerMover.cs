using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    private Player _playerCached;

    private void Awake()
    {
        _playerCached = GetComponent<Player>();
        EventBus<PlayerInputMessage>.Sub(Move);
    }

    private void OnDestroy()
    {
        EventBus<PlayerInputMessage>.Unsub(Move);
    }

    private void Move(PlayerInputMessage message)
    {
        var speed = _playerCached.MoveSpeed;
        var delta = new Vector3(speed * message.MovementDirection.x, 0, speed * message.MovementDirection.y) *
                    Time.deltaTime;
        transform.position += delta;
        transform.forward = new Vector3(message.AimDirection.x, 0, message.AimDirection.y);
    }
}