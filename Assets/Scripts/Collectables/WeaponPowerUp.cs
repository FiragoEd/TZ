using PowerUps;
using PowerUps.Behaviour;

namespace Collectables
{
    public class WeaponPowerUp : PowerUpComponentBase
    {
        public WeaponType Type;
        
        public override void ApplyPowerUp()
        {
            Player.Instance.ChangeWeapon(Type);
            Destroy(gameObject);
        }
    }
}