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

    public bool Attack(Transform user, Vector2 direction, out int hitCount)
    {
        var attackPoint = (Vector2)user.position + direction * attackOffset;

        //attackPoint += Vector3.up;

        var colliders = Physics2D.OverlapCircleAll(attackPoint, hitRadius, LayerMask);

        if (colliders.Length <= 0)
        {
            hitCount = 0;

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

        hitCount = colliders.Length;

        return true;
    }

    public void DrawGizmos(Transform user, Vector2 direction)
    {
        var attackPoint = (Vector2)user.position + direction * attackOffset;

        //attackPoint += Vector3.up;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint, hitRadius);
    }
}