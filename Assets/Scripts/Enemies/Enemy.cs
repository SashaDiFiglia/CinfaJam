using Sirenix.OdinInspector;
using Unity.Behavior;
using UnityEngine;
using Action = System.Action;

[RequireComponent(typeof(BehaviorGraphAgent))]
public class Enemy : MonoBehaviour, IHealth
{
    private static readonly int AttackKey = Animator.StringToHash("Attack");

    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Animator _animator;
    public Transform debugAggroRange;
    public Transform debugCloseRange;

    private BehaviorGraphAgent m_behaviourAgent;
    private Rigidbody2D _rigidbody2D;

    private Vector2 _previousPosition;
    public Vector2 CurrentDirection { get; private set; }
    public Vector2 PreviousDirection { get; private set; }

    [ShowInInspector, ReadOnly] private float _currentHealth;
    private bool _isDead;

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
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _currentHealth = _enemyData.MaxHealth;
        BehaviourAgent.Graph = _enemyData.BehaviorGraph;

        BehaviourAgent.Init();

        var controller = FindFirstObjectByType<CharacterController>();

        if (controller)
        {
            BehaviourAgent.BlackboardReference.SetVariableValue("Target", controller.transform);
        }

        BehaviourAgent.BlackboardReference.SetVariableValue("AggroRange", _enemyData.AggroRadius);
        BehaviourAgent.BlackboardReference.SetVariableValue("WalkSpeed", _enemyData.WalkSpeed);
        BehaviourAgent.BlackboardReference.SetVariableValue("CloseRange", _enemyData.Weapon.hitRadius);
        BehaviourAgent.BlackboardReference.SetVariableValue("AttackCooldown", _enemyData.AttackCooldown);
        BehaviourAgent.BlackboardReference.SetVariableValue("Enemy", this);

        debugAggroRange.localScale = Vector3.one * _enemyData.AggroRadius * 2;
        debugCloseRange.localScale = Vector3.one * _enemyData.Weapon.hitRadius * 2;
        BehaviourAgent.Start();
    }

    public void Attack()
    {
        if (_animator)
        {
            _animator.SetTrigger(AttackKey);
        }
    }

    private void FixedUpdate()
    {
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
        var direction = (newPosition - _previousPosition).normalized;

        if (direction.sqrMagnitude >= 0.1f)
        {
            PreviousDirection = direction;
        }

        CurrentDirection = direction;
        _previousPosition = newPosition;

        _rigidbody2D.MovePosition(newPosition);
    }
}