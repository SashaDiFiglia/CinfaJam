using Sirenix.OdinInspector;
using Unity.Behavior;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private BehaviorGraph _behaviorGraph;

    [Title("Stats")] [SerializeField] private float _maxHealth;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _aggroRadius;

    public BehaviorGraph BehaviorGraph => _behaviorGraph;
    public float MaxHealth => _maxHealth;
    public float WalkSpeed => _walkSpeed;
    public float AttackRate => _attackRate;
    public float AggroRadius => _aggroRadius;
}