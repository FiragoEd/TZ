using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PowerUps.Config
{
    [CreateAssetMenu(fileName = "WeaponPowerUpConfigSet", menuName = "Configs/PowerUps/WeaponPowerUpConfigSet",
        order = 0)]
    public class WeaponConfigSet : ScriptableObject
    {
        [ValidateInput("NoDuplicates", "Айтемы должны быть уникальными")]
        [SerializeField] private List<WeaponPowerUpConfig> _weaponPowerUpConfigs = new List<WeaponPowerUpConfig>();

        public List<WeaponPowerUpConfig> WeaponPowerUpConfigs => _weaponPowerUpConfigs;

        private bool NoDuplicates()
        {
            return _weaponPowerUpConfigs.Count == _weaponPowerUpConfigs.Select(e => e.Type).Distinct().Count();
        }
    }
}