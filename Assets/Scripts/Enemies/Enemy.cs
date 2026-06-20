using System;
using Sirenix.OdinInspector;
using Unity.Behavior;
using UnityEditor;
using UnityEngine;
using Action = System.Action;

[RequireComponent(typeof(BehaviorGraphAgent), typeof(CharacterAnimation))]
public class Enemy : MonoBehaviour, IHealth
{
    private static readonly int AttackKey = Animator.StringToHash("Attack");

    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Animator _animator;

    private Vector2 _lastPosition;
    public Vector2 CurrentDirection { get; private set; }
    public Vector2 LastFacingDirection { get; private set; } = Vector2.right;

    private BehaviorGraphAgent m_behaviourAgent;
    private CharacterAnimation _characterAnimation;
    private Rigidbody2D _rigidbody2D;

    [ShowInInspector, ReadOnly] private float _currentHealth;

    private bool _isDead;
    private bool _canMove = true;

    public event Action OnDeath;

    public BehaviorGraphAgent BehaviourAgent
    {
        get
        {
            return m_behaviourAgent ??= TryGetComponent<BehaviorGraphAgent>(out var agent)
                ? agent
                : gameObject.AddComponent<BehaviorGraphAgent>();
        }
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterAnimation = GetComponent<CharacterAnimation>();
    }

    private void Start()
    {
        _lastPosition = _rigidbody2D.position;
        Initialize();
    }

    private void Initialize()
    {
        _currentHealth = _enemyData.MaxHealth;
        BehaviourAgent.Graph = _enemyData.BehaviorGraph;

        BehaviourAgent.Init();

        var controller = FindFirstObjectByType<CharacterMovement>();

        if (controller)
        {
            BehaviourAgent.BlackboardReference.SetVariableValue("Target", controller.transform);
        }

        BehaviourAgent.BlackboardReference.SetVariableValue("AggroRange", _enemyData.AggroRadius);
        BehaviourAgent.BlackboardReference.SetVariableValue("WalkSpeed", _enemyData.WalkSpeed);
        BehaviourAgent.BlackboardReference.SetVariableValue("CloseRange", _enemyData.Weapon.attackOffset);
        BehaviourAgent.BlackboardReference.SetVariableValue("AttackCooldown", _enemyData.AttackCooldown);
        BehaviourAgent.BlackboardReference.SetVariableValue("Enemy", this);

        BehaviourAgent.Start();
    }

    public void Attack()
    {
        if (_animator)
        {
            _animator.SetTrigger(AttackKey);
        }

        if (_enemyData.Weapon.Attack(transform, LastFacingDirection, out var count))
        {
            Debug.Log("Enemy Attacked");
            _characterAnimation.PlayAttackAnimation();
        }
        else
        {
            Debug.Log("Enemy failed to attack");
        }
    }

    private void Update()
    {
        _canMove = false;
    }

    private void FixedUpdate()
    {
        _canMove = true;
        _rigidbody2D.linearVelocity = Vector2.zero;
        BehaviourAgent.Update();
    }

    public void TakeDamage(float damage)
    {
        if (_isDead)
        {
            return;
        }

        _currentHealth -= damage;

        if (_currentHealth > 0)
        {
            return;
        }

        _isDead = true;
        OnDeath?.Invoke();
    }

    public void Move(Vector2 newPosition)
    {
        if (!_canMove)
        {
            return;
        }

        var direction = (newPosition - _lastPosition).normalized;

        if (direction.sqrMagnitude >= 0.01f)
        {
            LastFacingDirection = direction;
        }

        CurrentDirection = direction;
        _lastPosition = newPosition;

        _rigidbody2D.MovePosition(newPosition);
    }

    private void OnDrawGizmos()
    {
        if (!_enemyData)
        {
            return;
        }

#if UNITY_EDITOR
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.forward, _enemyData.AggroRadius);
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.forward, _enemyData.Weapon.attackOffset);
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position + ((Vector3)LastFacingDirection * _enemyData.Weapon.attackOffset),
            Vector3.forward, _enemyData.Weapon.hitRadius);

#endif
    }
}