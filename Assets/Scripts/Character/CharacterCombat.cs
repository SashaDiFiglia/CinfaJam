using System;
using FMOD.Studio;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Weapon m_weapon;

    public Weapon Weapon
    {
        get => m_weapon;

        set
        {
            m_weapon = value;

            if (value != null)
            {
                _currentDurability = value.MaxDurability;
            }
        }
    }

    [Header("Sound")]
    public EventReference _attackSoundReference;

    public EventReference _weaponBreakReference;

    private EventInstance _attackSoundInstance;
    private EventInstance _weaponBreakInstance;

    [ShowInInspector, ReadOnly] private float _currentDurability;

    public float CurrentDurability => _currentDurability;

    public event Action OnAttackWithNoWeapon;
    public event Action OnWeaponBreak;

    private void Awake()
    {
        _attackSoundInstance = RuntimeManager.CreateInstance(_attackSoundReference);
        _weaponBreakInstance = RuntimeManager.CreateInstance(_weaponBreakReference);
    }

    private void Start()
    {
        if (Weapon != null)
        {
            _currentDurability = Weapon.MaxDurability;
        }
    }

    public void TryAttack(Vector2 direction)
    {
        if (Weapon == null)
        {
            OnAttackWithNoWeapon?.Invoke();

            return;
        }

        _attackSoundInstance.start();

        if (!Weapon.Attack(transform, direction, out var hitNumber))
        {
            return;
        }

        Debug.Log("Emico copito.");

        _currentDurability -= hitNumber;

        if (_currentDurability <= 0)
        {
            _weaponBreakInstance.start();

            OnWeaponBreak?.Invoke();

            Weapon = null;
        }
    }
}