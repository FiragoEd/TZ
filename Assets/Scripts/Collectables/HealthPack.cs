using PlayerComponents;
using PowerUps.Behaviour;
using UnityEngine;
using Utils.FadeText;

namespace Collectables
{
    public class HealthPack : PowerUpComponentBase
    {
        [SerializeField] private int _health;

        public override void ApplyPowerUp()
        {
            Player.Instance.Heal(_health);
            FadeTextSpawner.Instance.SpawnFadedText("+" + _health, transform.position);
            Destroy(gameObject);
        }
    }
}