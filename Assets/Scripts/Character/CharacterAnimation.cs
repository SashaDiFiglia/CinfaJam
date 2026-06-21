using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    private Vector2 _previousDirection;

    private CharacterState _characterState;
    private string _currentAnimationKey = " ";

    private bool _canTransition = true;

    private const string IdlePrefix = "Idle";
    private const string MovingPrefix = "Move";
    private const string AttackPrefix = "Attack";
    private const string TakeDamage = "TakeDamage";
    private const string Death = "Death";

    private const string FrontSuffix = "Front";
    private const string BackSuffix = "Back";
    private const string RightSuffix = "Right";
    private const string LeftSuffix = "Left";


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (_animator == null)
        {
            return;
        }

        _animator.Play(IdlePrefix + FrontSuffix);
    }

    public void ResetAnimations()
    {
        StopAllCoroutines();
        _coroutine = null;
        _canTransition = true;
        _characterState = CharacterState.Idle;
        _animator.Play(IdlePrefix + FrontSuffix);
    }

    public void ChangeState(CharacterState newState, bool canTransitionToNextState = true, float lockDuration = 0.5f)
    {
        if (_characterState == CharacterState.Dying)
        {
            return;
        }

        if (!_canTransition && newState != CharacterState.Dying)
        {
            return;
        }

        _canTransition = canTransitionToNextState;

        if (_animator == null)
        {
            return;
        }

        _characterState = newState;

        string newKey = GetDirectionalAnimationKey(_previousDirection, _characterState);
        bool snapState = false;

        switch (_characterState)
        {
            case CharacterState.Idle:
                if (newKey == _currentAnimationKey)
                {
                    return;
                }

                break;
            case CharacterState.Moving:
                if (newKey == _currentAnimationKey)
                {
                    return;
                }

                break;

            case CharacterState.Attacking:
                snapState = true;
                break;

            case CharacterState.TakingDamage:
                snapState = true;
                break;

            case CharacterState.Dying:
                _canTransition = false;
                break;
            default:
                break;
        }

        _currentAnimationKey = newKey;
        if (snapState)
        {
            _animator.Play(newKey, 0, 0f);
        }
        else
        {
            _animator.Play(newKey);
        }

        if (!canTransitionToNextState)
        {
            LockTransitionForSeconds(lockDuration);
        }
    }

    public void UpdateMovement(Vector2 movementVector)
    {
        if (movementVector.sqrMagnitude < 0.1f)
        {
            return;
        }

        _previousDirection = DirectionUtils.EvaluateDirection(movementVector, _previousDirection);
    }

    private string GetDirectionalAnimationKey(Vector2 direction, CharacterState characterState)
    {
        string statePrefix;
        switch (characterState)
        {
            case CharacterState.Idle:
                statePrefix = IdlePrefix;
                break;
            case CharacterState.Moving:
                statePrefix = MovingPrefix;
                break;
            case CharacterState.Attacking:
                statePrefix = AttackPrefix;
                break;
            case CharacterState.TakingDamage:
                statePrefix = TakeDamage;
                return statePrefix;

            case CharacterState.Dying:
                statePrefix = Death;
                return statePrefix;

            default:
                statePrefix = IdlePrefix;
                break;
        }

        string directionSuffix = (direction.x, direction.y) switch
        {
            (> 0.1f, _) => RightSuffix,
            (< -0.1f, _) => LeftSuffix,
            (_, > 0.1f) => BackSuffix,
            (_, < -0.1f) => FrontSuffix,
            _ => FrontSuffix
        };

        return statePrefix + directionSuffix;
    }

    private Coroutine _coroutine;

    private void LockTransitionForSeconds(float duration)
    {
        _coroutine ??= StartCoroutine(WaitCoroutine(duration));
    }

    private IEnumerator WaitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        _canTransition = true;
        _coroutine = null;
    }
}

public enum CharacterState
{
    Idle,
    Moving,
    Attacking,
    TakingDamage,
    Dying
}