using System;
using UnityEngine;

public class Trap_BulletSpawner : MonoBehaviour, ITrap {
	[SerializeField] private BulletSO bulletData;
	private Transform _spawnPoint;

	private void OnDrawGizmos() {
		if(_spawnPoint==null)
			_spawnPoint = transform.GetChild(0);
		Gizmos.DrawSphere(_spawnPoint.transform.position+Vector3.back*4, .2f);
	}

	private void Start() {
		_spawnPoint = transform.GetChild(0);
	}

	public void ActivateTrap() {
		GameObject bullet = Instantiate(this.bulletData.bulletPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
		bullet.name = $"Bullet_{_spawnPoint.name}";
		bullet.GetComponent<BulletController>().Setup(bulletData.bulletSpeed, bulletData.bulletDamage, bulletData.bulletLifetime, bulletData.canExplode, bulletData.canHitEnemies, bulletData.explosionRadius);
	}
}