using System;
using Unity.Behavior;
using UnityEngine;
using Action = System.Action;

[RequireComponent(typeof(BehaviorGraphAgent))]
public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyData _enemyData;
    public Transform aggroRange;
    public Transform closeRange;

    private BehaviorGraphAgent _behaviourAgent;

    private float _currentHealth;
    private bool _isDead;

    public event Action OnDeath;

    /// <summary>
    /// <param name="float"> Cooldown time</param>
    /// </summary>
    public event Action<float> OnAttackCooldownStarted;

    public BehaviorGraphAgent BehaviourAgent
    {
        get
        {
            return _behaviourAgent ??= TryGetComponent<BehaviorGraphAgent>(out var agent)
                ? agent
                : gameObject.AddComponent<BehaviorGraphAgent>();
        }
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
        BehaviourAgent.BlackboardReference.SetVariableValue("AggroRange", _enemyData.AggroRadius);
        BehaviourAgent.BlackboardReference.SetVariableValue("WalkSpeed", _enemyData.WalkSpeed);
        BehaviourAgent.BlackboardReference.SetVariableValue("CloseRange", _enemyData.Weapon.Range);
        BehaviourAgent.BlackboardReference.SetVariableValue("AttackCooldown", _enemyData.AttackCooldown);

        aggroRange.localScale = Vector3.one * _enemyData.AggroRadius * 2;
        closeRange.localScale = Vector3.one * _enemyData.Weapon.Range * 2;
    }

    public void Attack()
    {
        Debug.Log("Attacking");
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
}