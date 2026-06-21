using Sirenix.OdinInspector;
using Unity.Behavior;
using UnityEditor;
using UnityEngine;
using Action = System.Action;

[RequireComponent(typeof(BehaviorGraphAgent), typeof(CharacterAnimation), typeof(CharacterMovement))]
public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyData _enemyData;

    private BehaviorGraphAgent m_behaviourAgent;
    private CharacterAnimation _characterAnimation;
    private CharacterMovement _characterMovement;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;

    [ShowInInspector, ReadOnly] private float _currentHealth;

    private bool _isDead;
    private bool _canMove = true;
    private Vector2 _lastFacingDirection;

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
        _characterMovement = GetComponent<CharacterMovement>();
        _characterAnimation = GetComponent<CharacterAnimation>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _isDead = false;
        _collider2D.enabled = true;
        _currentHealth = _enemyData.MaxHealth;
        BehaviourAgent.Graph = _enemyData.BehaviorGraph;

        BehaviourAgent.Init();

        var controller = FindFirstObjectByType<CharacterInput>();

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

    [Button]
    private void Deactivate()
    {
        _collider2D.enabled = false;
        BehaviourAgent.End();
    }

    [Button]
    public void Activate()
    {
        Initialize();
        _characterAnimation.ResetAnimations();
    }

    public void Attack()
    {
        _characterAnimation.ChangeState(CharacterState.Attacking, false, 1f);
        _enemyData.Weapon.Attack(transform, _lastFacingDirection, out var count);
    }

    private void Update()
    {
        _canMove = false;
    }

    private void FixedUpdate()
    {
        _canMove = true;
        BehaviourAgent.Update();
    }

    public void TakeDamage(float damage)
    {
        if (_isDead)
        {
            return;
        }

        _characterAnimation.ChangeState(CharacterState.TakingDamage, false);

        _currentHealth -= damage;

        if (_currentHealth > 0)
        {
            return;
        }

        _isDead = true;
        OnDeath?.Invoke();
        Die();
    }

    private void Die()
    {
        _characterAnimation.ChangeState(CharacterState.Dying);
        Deactivate();
    }

    public void Move(Vector2 movementVector)
    {
        if (!_canMove || _isDead)
        {
            return;
        }

        if (movementVector.sqrMagnitude > 0.1f)
        {
            _lastFacingDirection = DirectionUtils.EvaluateDirection(movementVector.normalized, _lastFacingDirection);
        }

        _characterMovement.Move(movementVector, Time.fixedDeltaTime);
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
        Handles.DrawWireDisc(transform.position + ((Vector3)_lastFacingDirection * _enemyData.Weapon.attackOffset),
            Vector3.forward, _enemyData.Weapon.hitRadius);

#endif
    }
}