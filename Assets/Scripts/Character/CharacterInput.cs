using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    private PlayerInput _input;

    private CharacterController _controller;
    private PlayerCombat _combat;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _combat = GetComponent<PlayerCombat>();

        _input = new PlayerInput();
        _input.Enable();

        _input.Gameplay.Attack.performed += _ => HandleAttack();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var dir = _input.Gameplay.Move.ReadValue<Vector2>();

        if (dir != Vector2.zero)
        {
            _controller?.Move(dir, Time.deltaTime);
        }
    }

    private void HandleAttack()
    {
        _combat?.Attack();
    }
}