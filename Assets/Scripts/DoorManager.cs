using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private List<BracierInstance> braciers;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<Door> doors;

    private int aliveEnemies;
    private bool completed;

    private void Awake()
    {
        aliveEnemies = enemies.Count;

        foreach (var enemy in enemies)
        {
            enemy.OnDeath += OnEnemyDeath;
        }
    }

    private void OnDestroy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.OnDeath -= OnEnemyDeath;
            }
        }
    }

    private void OnEnemyDeath()
    {
        aliveEnemies--;

        CheckCompletion();
    }

    private void Update()
    {
        if (completed)
            return;

        CheckCompletion();
    }

    private void CheckCompletion()
    {
        if (completed)
            return;

        bool allBraciersLit = braciers.All(b => b.GetInstanceData().hasBeenLit);
        bool allEnemiesDead = aliveEnemies <= 0;

        if (allBraciersLit && allEnemiesDead)
        {
            completed = true;

            foreach (var door in doors)
            {
                door.Open();
            }
        }
    }
}