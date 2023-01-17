using PowerUps;
using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
	public abstract WeaponType Type { get; }
	public GameObject Model;

	protected virtual void Awake()
	{
		GetComponent<Player>().OnWeaponChange.AddListener(Change);
	}

	protected virtual void OnDestroy()
	{
		GetComponent<Player>().OnWeaponChange.RemoveListener(Change);
		EventBus<PlayerInputMessage>.Unsub(Fire);
	}

	protected void Change(WeaponType type)
	{
		EventBus<PlayerInputMessage>.Unsub(Fire);
		if (type == Type)
		{
			EventBus<PlayerInputMessage>.Sub(Fire);
		}
		Model.SetActive(type == Type);
	}

	protected abstract void Fire(PlayerInputMessage message);
}