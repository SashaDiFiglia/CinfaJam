using System;
using UnityEngine;

public class BulletSpawnerTrap : MonoBehaviour, ITrap
{
    [SerializeField] private BulletDataSO bulletData;
    private Transform _spawnPoint;
    
    private void Start()
    {
        _spawnPoint = transform.GetChild(0);
    }

    public void ActivateTrap()
    {
        GameObject bullet = Instantiate(bulletData.bulletPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
        bullet.name = $"Bullet_{_spawnPoint.name}";
        bullet.GetComponent<BulletController>().Setup(bulletData.bulletSpeed, bulletData.bulletDamage, bulletData.bulletLifetime, bulletData.canExplode, bulletData.canHitEnemies, bulletData.explosionRadius);
    }
}
