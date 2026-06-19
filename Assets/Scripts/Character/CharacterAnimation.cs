using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;

    private const string IDLE_FRONT = "IdleFront";
    private const string IDLE_BACK = "IdleBack";
    private const string IDLE_RIGHT = "IdleRight";
    private const string IDLE_LEFT = "IdleLeft";

    private const string MOVEMENT_FRONT = "MoveFront";
    private const string MOVEMENT_BACK = "MoveBack";
    private const string MOVEMENT_RIGHT = "MoveRight";
    private const string MOVEMENT_LEFT = "MoveLeft";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.Play(MOVEMENT_FRONT);
    }
}