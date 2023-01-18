using NTC.Global.Pool;
using UnityEngine;

namespace Systems
{
	public class MobSpawner : Handler<SpawnMobMessage>
	{
		public Mob.Mob[] Prefabs;

		protected override void Awake()
		{
			base.Awake();
			EventBus.Sub(() => { EventBus<SpawnMobMessage>.Unsub(HandleMessage);},EventBus.PLAYER_DEATH);
		}

		public override void HandleMessage(SpawnMobMessage message)
		{
			var position = new Vector3(Random.value * 11 - 6,1,Random.value * 11 - 6);
			NightPool.Spawn(Prefabs[message.Type], position, Quaternion.identity);
		}
	}
}