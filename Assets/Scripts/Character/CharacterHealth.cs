using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IHealth
{
    public float MaxHealth;
    [ReadOnly] public float CurrentHealth;

    public event Action<float> OnHealthChange;
    public event Action OnDeath;

    private void Start()
    {
        FillHealth();
    }

    [Button]
    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);

        OnHealthChange?.Invoke(CurrentHealth);

        if (CurrentHealth == 0)
        {
            OnDeath?.Invoke();
        }
    }

    [Button]
    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);

        OnHealthChange?.Invoke(CurrentHealth);
    }

    public void Respawn()
    {
        FillHealth();
    }

    private void FillHealth()
    {
        CurrentHealth = MaxHealth;
        
        OnHealthChange?.Invoke(CurrentHealth);
    }
}