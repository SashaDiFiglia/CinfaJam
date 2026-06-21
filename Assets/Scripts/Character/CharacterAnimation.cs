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

    private const string IdleFront = "IdleFront";
    private const string IdleBack = "IdleBack";
    private const string IdleRight = "IdleRight";
    private const string IdleLeft = "IdleLeft";

    private const string MovementFront = "MoveFront";
    private const string MovementBack = "MoveBack";
    private const string MovementRight = "MoveRight";
    private const string MovementLeft = "MoveLeft";

    private const string AttackFront = "AttackFront";
    private const string AttackBack = "AttackBack";
    private const string AttackRight = "AttackRight";
    private const string AttackLeft = "AttackLeft";

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

        _animator.Play(IdleFront);
    }

    public void ChangeState(CharacterState newState, bool canTransitionToNextState = true, float lockDuration = 0.5f)
    {
        if (!_canTransition)
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
           
            default:
                break;
        }

        _currentAnimationKey = newKey;
        _animator.Play(newKey);

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
        string statePrefix = characterState switch
        {
            CharacterState.Idle => "Idle",
            CharacterState.Moving => "Move",
            CharacterState.Attacking => "Attack",
            CharacterState.TakingDamage => "TakingDamage",
            CharacterState.Dying => "Dying",
            _ => "Idle"
        };

        string directionSuffix = (direction.x, direction.y) switch
        {
            (> 0.1f, _) => "Right",
            (< -0.1f, _) => "Left",
            (_, > 0.1f) => "Back",
            (_, < -0.1f) => "Front",
            _ => "Front"
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