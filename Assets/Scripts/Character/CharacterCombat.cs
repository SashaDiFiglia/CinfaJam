using System;
using FMOD.Studio;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Weapon m_weapon;

    [SerializeField] private GameObject _weaponHolder;

    public Weapon Weapon
    {
        get => m_weapon;

        set
        {
            m_weapon = value;

            if (value != null)
            {
                _currentDurability = value.MaxDurability;

                _weaponHolder.SetActive(true);
            }
        }
    }

    private CharacterAnimation _characterAnimation;

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
        _characterAnimation = GetComponent<CharacterAnimation>();
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
        _characterAnimation.ChangeState(CharacterState.Attacking, false, 0.1f);

        if (!Weapon.Attack(transform, direction, out var hitNumber))
        {
            return;
        }

        _currentDurability -= hitNumber;

        if (_currentDurability <= 0)
        {
            _weaponBreakInstance.start();

            OnWeaponBreak?.Invoke();

            _weaponHolder.SetActive(false);

            Weapon = null;
        }
    }

    #region DEBUG

    [Button]
    private void DecreaseDurability(float amount)
    {
        _currentDurability -= amount;
        
        if (_currentDurability <= 0)
        {
            _weaponBreakInstance.start();

            OnWeaponBreak?.Invoke();

            _weaponHolder.SetActive(false);

            Weapon = null;
        }
    }

    [Button]
    private void IncreaseDurability(float amount)
    {
        _currentDurability += amount;
    }

    #endregion
}