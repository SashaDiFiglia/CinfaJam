using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction, float deltaTime)
    {
        var moveDir = new Vector3(direction.x, direction.y, 0).normalized;

        var targetPos = transform.position + moveDir * (deltaTime * _moveSpeed);

        _rb.MovePosition(targetPos);
    }
}