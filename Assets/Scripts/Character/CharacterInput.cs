using System;
using UnityEditor;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    private PlayerInput _input;

    private CharacterMovement _movement;
    private CharacterCombat _combat;

    private Vector2 _currentDirection;
    private Vector2 _prevDirection;

    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        _combat = GetComponent<CharacterCombat>();

        _input = new PlayerInput();
        _input.Enable();

        _input.Gameplay.Attack.performed += _ => HandleAttack();
    }

    private void FixedUpdate()
    {
        HandleMovement();

        CacheDirection();
    }

    private void CacheDirection()
    {
        var input = _input.Gameplay.Move.ReadValue<Vector2>();

        if (input != Vector2.zero)
        {
            _prevDirection = input;
        }

        _currentDirection = input;
    }

    private void HandleMovement()
    {
        var input = _input.Gameplay.Move.ReadValue<Vector2>();

        if (input != Vector2.zero)
        {
            _movement?.Move(input, Time.fixedDeltaTime);
        }
    }

    private void HandleAttack()
    {
        var attackDir = GetFourWayDirection(_prevDirection);
        _combat?.TryAttack(attackDir);
    }

    private Vector2 GetFourWayDirection(Vector2 inputDirection)
    {
        if (inputDirection == Vector2.zero)
        {
            return Vector2.down;
        }

        return Mathf.Abs(inputDirection.x) >= Mathf.Abs(inputDirection.y)
            ? new Vector2(Mathf.Sign(inputDirection.x), 0)
            : new Vector2(0, Mathf.Sign(inputDirection.y));
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        var attackPoint = (Vector2)transform.position + GetFourWayDirection(_prevDirection) * 1f;
        var movePoint = (Vector2)transform.position + _prevDirection * 0.7f;

        Handles.color = Color.red;
        Handles.DrawWireDisc(attackPoint, Vector3.forward, 0.3f);

        Handles.color = Color.green;
        Handles.DrawWireDisc(movePoint, Vector3.forward, 0.1f);
    }

#endif
}