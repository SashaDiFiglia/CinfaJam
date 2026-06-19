using Sirenix.OdinInspector;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private BehaviorGraph _behaviorGraph;

    [Title("Stats")] [SerializeField] private float _maxHealth;
    [SerializeField] private float _walkSpeed;
    [FormerlySerializedAs("_attackRate")] [SerializeField] private float _attackCooldown;
    [SerializeField] private float _aggroRadius;
    [SerializeField] private Weapon _weapon;
    
    public BehaviorGraph BehaviorGraph => _behaviorGraph;
    public float MaxHealth => _maxHealth;
    public float WalkSpeed => _walkSpeed;
    public float AttackCooldown => _attackCooldown;
    public float AggroRadius => _aggroRadius;
    public Weapon Weapon => _weapon;
}