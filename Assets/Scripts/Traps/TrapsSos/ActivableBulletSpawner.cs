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
    private Transform _spawnPoint;

    protected override void OnSetup()
    { _spawnPoint = trapController.gameObject.transform.GetChild(0); }
    
    
    public override void ActivateTrap()
    {
        GameObject bullet = Instantiate(bulletPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
        bullet.GetComponent<BulletController>().Setup(bulletSpeed, bulletDamage, bulletLifetime);
    }

    public override void DeactivateTrap()
    {
        //no need to deactivate anything here.
    }
}
