using UnityEngine;

[ CreateAssetMenu(fileName = "New Activable Wall Bullet", menuName = "Traps/Activable Bullet Spawner")]
public class ActivableBulletSpawner: AActivableTrap
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    
    public override void ActivateTrap()
    {
        Instantiate(bulletPrefab, trapController.transform.position, Quaternion.identity).
            GetComponent<Rigidbody2D>().linearVelocity = trapController.transform.forward * bulletSpeed;
    }

    public override void DeactivateTrap()
    {
        //no need to deactivate anything here.
    }
}
