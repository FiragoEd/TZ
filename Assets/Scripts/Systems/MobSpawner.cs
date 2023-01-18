using MobComponents;
using NTC.Global.Pool;
using UnityEngine;

namespace Systems
{
    public class MobSpawner : Handler<SpawnMobMessage>
    {
        [SerializeField] private Mob[] _prefabs;

        protected override void Awake()
        {
            base.Awake();
            EventBus.Sub(UnSubEvent, EventBus.PLAYER_DEATH);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventBus.Unsub(UnSubEvent, EventBus.PLAYER_DEATH);
        }

        public override void HandleMessage(SpawnMobMessage message)
        {
            var position = new Vector3(Random.value * 11 - 6, 1, Random.value * 11 - 6);
            NightPool.Spawn(_prefabs[message.Type], position, Quaternion.identity);
        }

        private void UnSubEvent()
        {
            EventBus<SpawnMobMessage>.Unsub(HandleMessage);
        }
    }
}