using System;
using UnityEngine;

public class CharacterAnimationChanger : MonoBehaviour
{
    private CharacterAnimation _characterAnimation;
    private CharacterHealth _characterHealth;


    private void Awake()
    {
        _characterAnimation = GetComponent<CharacterAnimation>();
        _characterHealth = GetComponent<CharacterHealth>();
    }

    private void Start()
    {
        _characterHealth.OnHealthChange += _ => TriggerDamageAnim();
        _characterHealth.OnDeath += TriggerDeathAnimation;
    }

    private void TriggerDeathAnimation()
    {
        // _characterAnimation.ChangeState(CharacterState.Dying);
    }

    private void TriggerDamageAnim()
    {
        _characterAnimation.ChangeState(CharacterState.TakingDamage, false);
    }
}