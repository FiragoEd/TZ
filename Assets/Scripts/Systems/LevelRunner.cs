using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Systems
{
    public class LevelRunner : MonoBehaviour
    {
        [SerializeField] private int _maxMobCount = 5;
        [SerializeField] private float _spawnInterval = 2f;

        private List<LevelData> _levelDatas = null;

        private int _mobsCount = 0;


        private void Awake()
        {
            _levelDatas = new List<LevelData>(Resources.LoadAll<LevelData>("Data"));
            EventBus.Sub(MobKilled, EventBus.MOB_KILLED);
            EventBus<SpawnMobMessage>.Sub(MobSpawned);
            EventBus<LoadLevelMessage>.Sub(LoadLevelMessage);
        }

        private void Start()
        {
            EventBus<LoadLevelMessage>.Pub(new LoadLevelMessage(0));
        }

        private void OnDestroy()
        {
            EventBus.Unsub(MobKilled, EventBus.MOB_KILLED);
            EventBus<SpawnMobMessage>.Unsub(MobSpawned);
            EventBus<LoadLevelMessage>.Unsub(LoadLevelMessage);
        }

        private void LoadLevel(int index)
        {
            var level = _levelDatas.Find(l => l.Index == index);
            if (level == null)
            {
                EventBus.Pub(EventBus.PLAYER_WON);
                return;
            }

            StartCoroutine(Waves(level.WaveData, level.WaveInterval, level.Index));
            EventBus<FieldCreateMessage>.Pub(new FieldCreateMessage()
            {
                Field = level.MapCell
            });
        }

        private void MobKilled()
        {
            _mobsCount--;
        }

        private void MobSpawned(SpawnMobMessage message)
        {
            _mobsCount++;
        }

        private void LoadLevelMessage(LoadLevelMessage message)
        {
            LoadLevel(message.LevelIndex);
        }

        private IEnumerator Waves(List<WaveData> waveDatas, float interval, int level)
        {
            foreach (var waveData in waveDatas)
            {
                foreach (var keyValue in waveData.WaveMobNCount)
                {
                    for (int i = 0; i < keyValue.y; i++)
                    {
                        while (_mobsCount >= _maxMobCount)
                        {
                            yield return new WaitForSeconds(_spawnInterval);
                        }

                        EventBus<SpawnMobMessage>.Pub(new SpawnMobMessage()
                        {
                            Type = keyValue.x
                        });
                        yield return new WaitForSeconds(_spawnInterval);
                    }
                }

                yield return new WaitForSeconds(interval);
            }

            yield return new WaitForSeconds(interval);
            EventBus<LoadLevelMessage>.Pub(new LoadLevelMessage(level + 1));
        }
    }
}