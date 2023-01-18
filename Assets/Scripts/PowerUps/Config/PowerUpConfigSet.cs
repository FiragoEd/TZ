using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PowerUps.Config
{
    [CreateAssetMenu(fileName = "PowerUpConfigSet", menuName = "Configs/PowerUps/PowerUpConfigSet", order = 0)]
    public class PowerUpConfigSet : ScriptableObject
    {
        [ValidateInput("NoDuplicates", "Айтемы должны быть уникальными")]
        [SerializeField] private List<PowerUpConfig> _powerUpConfigs = new List<PowerUpConfig>();

        public List<PowerUpConfig> PowerUpConfigs => _powerUpConfigs;

        private bool NoDuplicates()
        {
            return _powerUpConfigs.Count == _powerUpConfigs.Select(e => e.Type).Distinct().Count();
        }
    }
}