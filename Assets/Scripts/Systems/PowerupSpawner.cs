using System.Collections.Generic;
using PowerUps;
using PowerUps.Config;
using UnityEngine;
using Utils;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private PowerUpConfigSet _powerUpConfigSet;
    [SerializeField] private WeaponConfigSet _weaponConfigSet;

    private WeaponType _currentWeaponType = WeaponType.Rifle;
    private readonly Dictionary<PowerUpType, float> _powerUpsWeight = new Dictionary<PowerUpType, float>();
    private readonly Dictionary<WeaponType, float> _weaponPowerUpsWeight = new Dictionary<WeaponType, float>();

    private void Awake()
    {
        EventBus.Sub(Handle, EventBus.MOB_KILLED);
        Player.Instance.OnWeaponChange += HandleChangeWeapon;
    }
    
    private void Start()
    {
        InitWeightDictionaries();
    }

    private void InitWeightDictionaries()
    {
        foreach (var powerUpConfig in _powerUpConfigSet.PowerUpConfigs)
        {
            _powerUpsWeight.Add(powerUpConfig.Type, powerUpConfig.Weight);
        }

        foreach (var weaponPower in _weaponConfigSet.WeaponPowerUpConfigs)
        {
            _weaponPowerUpsWeight.Add(weaponPower.Type, weaponPower.Weight);
        }
    }

    private void Handle()
    {
        Spawn(PickRandomPosition());
    }
    
    private void HandleChangeWeapon(WeaponType obj)
    {
        _currentWeaponType = obj;
    }

    private Vector3 PickRandomPosition()
    {
        var vector3 = new Vector3
        {
            x = Random.value * 11 - 6,
            z = Random.value * 11 - 6,
            y = 0.7f
        };
        return vector3;
    }


    private void Spawn(Vector3 position)
    {
        var type = RandomUtils.GetRandomFromArrayWithWeight(_powerUpsWeight);
        if (type != PowerUpType.ChangeWeapon)
        {
            Instantiate(_powerUpConfigSet.PowerUpConfigs.Find(x => x.Type == type).Prefab, position,
                Quaternion.identity); //Можно закешировать чтобы не тягать запросы каждый раз
        }
        else
        {
            var tempDict = _weaponPowerUpsWeight;
            tempDict.Remove(_currentWeaponType);
            var weaponType = RandomUtils.GetRandomFromArrayWithWeight(tempDict);
            Instantiate(_weaponConfigSet.WeaponPowerUpConfigs.Find(x => x.Type == weaponType).Prefab, position,
                Quaternion.identity); //Можно закешировать чтобы не тягать запросы каждый раз
        }
    }
}
