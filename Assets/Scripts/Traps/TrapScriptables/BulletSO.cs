using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Traps/Data/Bullet")]
public class BulletSO : ScriptableObject
{
    [Header("Spawner Properties")]
    public GameObject bulletPrefab;
    public float bulletSpeed;

    [Header("Bullet Properties")]
    public bool canExplode;

    public float explosionRadius;
    public bool canHitEnemies;
    public float bulletDamage;
    public float bulletLifetime;
}

