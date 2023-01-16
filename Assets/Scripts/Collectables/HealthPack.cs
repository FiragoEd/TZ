using PowerUps.Behaviour;
using UnityEngine;

namespace Collectables
{
    public class HealthPack : PowerUpComponentBase
    {
        [SerializeField] private int Health;
        
        public override void ApplyPowerUp()
        {
            Player.Instance.Heal(Health);
            Destroy(gameObject);
        }
    }
}