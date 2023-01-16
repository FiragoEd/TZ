using UnityEngine;

namespace PowerUps.Config
{
    [CreateAssetMenu(fileName = "WeaponPowerUpConfig", menuName = "Configs/PowerUps/WeaponPowerUpConfig", order = 0)]
    public class WeaponPowerUpConfig : ScriptableObject
    {
        [SerializeField] private WeaponType _type;

        [SerializeField]
        [Range(0, 100)] private float _weight;

        [SerializeField]
        private GameObject _prefab; // При наличии адрессоблов оптимальнее использовать AssetReferenceGameObject

        public WeaponType Type => _type;
        public float Weight => _weight;
        public GameObject Prefab => _prefab;
    }
}