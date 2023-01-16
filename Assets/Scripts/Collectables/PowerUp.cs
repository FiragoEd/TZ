using PowerUps.Behaviour;
using UnityEngine;

namespace Collectables
{
    public class PowerUp : PowerUpComponentBase
    {
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private float _moveSpeed;

        public override void ApplyPowerUp()
        {
            Player.Instance.Upgrade(_health, _damage, _moveSpeed);
            Destroy(gameObject);
        }
    }
}