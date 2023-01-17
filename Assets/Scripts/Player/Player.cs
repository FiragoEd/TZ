using PowerUps;
using PowerUps.Behaviour;
using UnityEngine;
using Utils;
using Utils.Events;
using Event = Utils.Events.Event;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float Damage = 1;
    public float MoveSpeed = 3.5f;
    public float Health = 3;
    public float MaxHealth = 3;


    #region Events

    private readonly InvokableEvent<WeaponType> _onWeaponChange = new InvokableEvent<WeaponType>();
    public Event<WeaponType> OnWeaponChange => _onWeaponChange;
    
    private readonly InvokableEvent<DeltaHP> _onHPChange = new InvokableEvent<DeltaHP>();
    public Event<DeltaHP> OnHPChange => _onHPChange;
    
    private readonly InvokableEvent _onUpgrade = new InvokableEvent();
    public Event OnUpgrade => _onUpgrade;

    #endregion

  
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void TakeDamage(float amount)
    {
        if (Health <= 0)
            return;
        Health -= amount;
        if (Health <= 0)
        {
            EventBus.Pub(EventBus.PLAYER_DEATH);
        }

        _onHPChange?.Invoke(new DeltaHP()
        {
            currentHP = Health,
            deltaHP = -amount
        });
    }

    public void Heal(float amount)
    {
        if (Health <= 0)
            return;
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        _onHPChange?.Invoke(new DeltaHP()
        {
            currentHP = Health,
            deltaHP = amount
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PowerUpComponentBase>())
            other.GetComponent<PowerUpComponentBase>().ApplyPowerUp();
    }

    public void Upgrade(float hp, float dmg, float ms)
    {
        Damage += dmg;
        Health += hp;
        MaxHealth += hp;
        MoveSpeed += ms;
        _onUpgrade?.Invoke();
        _onHPChange?.Invoke(new DeltaHP()
        {
            currentHP = Health,
            deltaHP = 0
        });
    }

    public void ChangeWeapon(WeaponType type)
    {
        _onWeaponChange?.Invoke(type);
    }
}