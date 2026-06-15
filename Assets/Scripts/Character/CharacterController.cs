using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public void Move(Vector2 direction, float deltaTime)
    {
        var moveDir = new Vector3(direction.x, 0, direction.y).normalized;

        transform.position += moveDir * (deltaTime * _moveSpeed);
        transform.forward = moveDir;
    }
}