using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public CheckpointManager _checkPointManager;

    private CharacterHealth _character;

    private List<Enemy> _enemies = new List<Enemy>();

    private List<WeaponPickUp> _weapons = new List<WeaponPickUp>();

    private List<HealthPickUp> _potions = new List<HealthPickUp>();

    private void Start()
    {
        _enemies.AddRange(FindObjectsByType<Enemy>(default));

        _weapons.AddRange(FindObjectsByType<WeaponPickUp>(default));

        _potions.AddRange(FindObjectsByType<HealthPickUp>(default));

        _checkPointManager = FindFirstObjectByType<CheckpointManager>();

        _character = FindFirstObjectByType<CharacterHealth>();

        _character.OnDeath += RespawnPlayer;
    }

    private void RespawnPlayer()
    {
        _character.transform.position = _checkPointManager.LastCheckPoint.transform.position;

        _character.Respawn();

        foreach (var enemy in _enemies)
        {
            enemy.Activate();
        }

        foreach (var weapon in _weapons)
        {
            weapon.Reset();
        }

        foreach (var potion in _potions)
        {
            potion.Reset();
        }
    }
}