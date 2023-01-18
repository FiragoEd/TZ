using PowerUps.Behaviour;
using UnityEngine;
using Utils.FadeText;

namespace Collectables
{
    public class HealthPack : PowerUpComponentBase
    {
        [SerializeField] private int Health;
        
        public override void ApplyPowerUp()
        {
            Player.Instance.Heal(Health);
            FadeTextSpawner.Instance.SpawnFadedText("+" + Health, transform.position);
            Destroy(gameObject);
        }
    }
}