using UnityEngine;

namespace PowerUps.Config
{
    [CreateAssetMenu(fileName = "PowerUpConfig", menuName = "Configs/PowerUps/PowerUpConfig", order = 0)]
    public class PowerUpConfig : ScriptableObject
    {
        [SerializeField] private PowerUpType _type;

        [SerializeField]
        [Range(0, 100)] private float _weight;

        [SerializeField]
        private GameObject _prefab; // При наличии адрессоблов оптимальнее использовать AssetReferenceGameObject

        public PowerUpType Type => _type;
        public float Weight => _weight;
        public GameObject Prefab => _prefab;
    }
}