using System;
using UnityEditor;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    private PlayerInput _input;

    private CharacterController _controller;
    private CharacterCombat _combat;

    private Vector2 _currentDirection;
    private Vector2 _prevDirection;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _combat = GetComponent<CharacterCombat>();

        _input = new PlayerInput();
        _input.Enable();

        _input.Gameplay.Attack.performed += _ => HandleAttack();
    }

    private void Update()
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
            _controller?.Move(input, Time.deltaTime);
        }
    }

    private void HandleAttack()
    {
        _combat?.TryAttack(_prevDirection);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        var point = (Vector2)transform.position + _prevDirection * 1f;

        Handles.color = Color.red;
        Handles.DrawWireDisc(point, Vector3.forward, 0.5f);
    }

#endif
}