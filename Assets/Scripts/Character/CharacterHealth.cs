using System;
using FMOD.Studio;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IHealth
{
    [Header("Sound")]
    [SerializeField] private EventReference _deathSound;

    public float MaxHealth;
    [ReadOnly] public float CurrentHealth;

    public event Action<float> OnHealthChange;
    public event Action OnDeath;

    private EventInstance _deathSoundInstance;

    private void Start()
    {
        FillHealth();

        _deathSoundInstance = RuntimeManager.CreateInstance(_deathSound);
    }

    [Button]
    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);

        OnHealthChange?.Invoke(CurrentHealth);

        if (CurrentHealth == 0)
        {
            _deathSoundInstance.start();

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