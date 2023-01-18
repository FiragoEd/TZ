using PlayerComponents;
using PowerUps;
using PowerUps.Behaviour;
using UnityEngine;

namespace Collectables
{
    public class WeaponPowerUp : PowerUpComponentBase
    {
        [SerializeField] private WeaponType _type;

        public override void ApplyPowerUp()
        {
            Player.Instance.ChangeWeapon(_type);
            Destroy(gameObject);
        }
    }
}