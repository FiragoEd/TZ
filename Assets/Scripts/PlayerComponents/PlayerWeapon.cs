using PowerUps;
using UnityEngine;

namespace PlayerComponents
{
	[RequireComponent(typeof(Player))]
	public abstract class PlayerWeapon : MonoBehaviour
	{
		protected abstract WeaponType _type { get; }
		[SerializeField] protected GameObject Model;

		protected Player _player;

		protected virtual void Awake()
		{
			_player = GetComponent<Player>();
			_player.OnWeaponChange.AddListener(Change);
		}

		protected virtual void OnDestroy()
		{
			_player.OnWeaponChange.RemoveListener(Change);
			EventBus<PlayerInputMessage>.Unsub(Fire);
		}

		private void Change(WeaponType type)
		{
			EventBus<PlayerInputMessage>.Unsub(Fire);
			if (type == _type)
			{
				EventBus<PlayerInputMessage>.Sub(Fire);
			}
			Model.SetActive(type == _type);
		}

		protected abstract void Fire(PlayerInputMessage message);
	}
}