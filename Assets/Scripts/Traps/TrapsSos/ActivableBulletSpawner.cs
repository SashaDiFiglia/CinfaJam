using UnityEngine;

[ CreateAssetMenu(fileName = "New Bullet Spawner", menuName = "Traps/Activable Bullet Spawner")]
public class ActivableBulletSpawner: AActivableTrap
{
    [Header("Spawner Properties")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [Header("Bullet Properties")]
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletLifetime;
    
    
    public override void ActivateTrap()
    {
        GameObject bullet = Instantiate(bulletPrefab, trapController.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = trapController.transform.forward * bulletSpeed;
        bullet.GetComponent<Bullet>().Setup(bulletDamage, bulletLifetime);
    }

    public override void DeactivateTrap()
    {
        //no need to deactivate anything here.
    }
}
