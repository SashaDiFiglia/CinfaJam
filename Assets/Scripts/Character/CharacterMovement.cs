using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private CharacterAnimation _characterAnimation;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _characterAnimation = GetComponent<CharacterAnimation>();
    }

    public void Move(Vector2 direction, float deltaTime)
    {
        CharacterState state = direction.sqrMagnitude < 0.01f ? CharacterState.Idle : CharacterState.Moving;
        _characterAnimation.ChangeState(state);
        _characterAnimation.UpdateMovement(direction);
        
        var moveDir = direction.normalized;

        var targetPos = (Vector2)transform.position + moveDir * (deltaTime * _moveSpeed);

        _rb.MovePosition(targetPos);
        _rb.linearVelocity = Vector2.zero;
    }
}