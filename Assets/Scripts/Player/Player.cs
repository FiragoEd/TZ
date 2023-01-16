using System;
using PowerUps;
using PowerUps.Behaviour;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float Damage = 1;
    public float MoveSpeed = 3.5f;
    public float Health = 3;
    public float MaxHealth = 3;
    
    public Action<WeaponType> OnWeaponChange = null;
    public Action<float, float> OnHPChange = null;
    public Action OnUpgrade = null;

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
        OnHPChange?.Invoke(Health, -amount);
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
        OnHPChange?.Invoke(Health, amount);
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
        OnUpgrade?.Invoke();
        OnHPChange?.Invoke(Health, 0);
    }

    public void ChangeWeapon(WeaponType type)
    {
        OnWeaponChange?.Invoke(type);
    }
}