using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    private Vector2 _newDirection;
    private Vector2 _previousDirection;
    private Vector2 _previousPosition;

    private const string IdleFront = "IdleFront";
    private const string IdleBack = "IdleBack";
    private const string IdleRight = "IdleRight";
    private const string IdleLeft = "IdleLeft";

    private const string MovementFront = "MoveFront";
    private const string MovementBack = "MoveBack";
    private const string MovementRight = "MoveRight";
    private const string MovementLeft = "MoveLeft";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _previousPosition = transform.position;
        _animator.Play(IdleFront);
    }

    private void FixedUpdate()
    {
        Vector2 currentDirection = ((Vector2)transform.position - _previousPosition).normalized;
        _previousPosition = transform.position;

        _newDirection = EvaluateDirection(currentDirection);

        if (_newDirection == _previousDirection)
        {
            return;
        }

        UpdateAnimation(_newDirection);

        _previousDirection = _newDirection;
    }

    private const float HysteresisBuffer = 0.1f;

    private Vector2 EvaluateDirection(Vector2 currentDirection)
    {
        if (currentDirection == Vector2.zero)
        {
            return Vector2.zero;
        }

        float absX = Mathf.Abs(currentDirection.x);
        float absY = Mathf.Abs(currentDirection.y);

        if (_previousDirection == Vector2.right || _previousDirection == Vector2.left)
        {
            absY -= HysteresisBuffer;
        }
        else if (_previousDirection == Vector2.up || _previousDirection == Vector2.down)
        {
            absX -= HysteresisBuffer;
        }

        if (absX >= absY)
        {
            return currentDirection.x > 0 ? Vector2.right : Vector2.left;
        }

        return currentDirection.y > 0 ? Vector2.up : Vector2.down;
    }

    private void UpdateAnimation(Vector2 direction)
    {
        string animatorKey = direction switch
        {
            _ when direction == Vector2.up => MovementBack,
            _ when direction == Vector2.down => MovementFront,
            _ when direction == Vector2.right => MovementRight,
            _ when direction == Vector2.left => MovementLeft,
            _ => IdleFront
        };

        _animator.Play(animatorKey);
    }
}