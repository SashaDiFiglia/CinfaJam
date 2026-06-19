using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Equipment/New Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Weapon Stats")]
    public int Damage = 10;

    public float MaxDurability;

    [Header("Hitbox Settings")]
    public float attackOffset = 1.0f;

    public float hitRadius = 0.5f;
    public LayerMask LayerMask;

    public bool Attack(Transform user, out int enemyCount)
    {
        var attackPoint = user.position + user.forward * attackOffset;

        attackPoint += Vector3.up;

        var colliders = Physics.OverlapSphere(attackPoint, hitRadius, LayerMask);

        if (colliders.Length <= 0)
        {
            enemyCount = 0;

            return false;
        }

        foreach (var hit in colliders)
        {
            if (hit.TryGetComponent<IHealth>(out var health))
            {
                Debug.Log("Colpito");
                health.TakeDamage(Damage);
            }
        }

        enemyCount = colliders.Length;

        return true;
    }

    public void DrawGizmos(Transform user)
    {
        var attackPoint = user.position + user.forward * attackOffset;

        attackPoint += Vector3.up;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint, hitRadius);
    }
}