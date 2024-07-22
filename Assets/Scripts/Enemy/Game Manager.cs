using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<EnemyMovement> enemies = new List<EnemyMovement>();

    public void RegisterEnemy(EnemyMovement enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    public void UnregisterEnemy(EnemyMovement enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    public void UpgradeAllEnemiesDamage()
    {
        foreach (var enemy in enemies)
        {
            enemy.UpgradeDamage();
        }
        Debug.Log("All enemies upgraded.");
    }
}
