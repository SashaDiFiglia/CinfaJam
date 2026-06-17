using Unity.Behavior;
using UnityEngine;
using Action = System.Action;

[RequireComponent(typeof(BehaviorGraphAgent))]
public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyData _enemyData;
    private BehaviorGraphAgent _behaviourAgent;

    private float _currentHealth;
    private bool _isDead;

    public event Action OnDeath;

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