using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IHealth
{
    public float MaxHealth;
    public float CurrentHealth;

    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);

        if (CurrentHealth == 0)
        {
            OnDeath?.Invoke();
        }
    }
}