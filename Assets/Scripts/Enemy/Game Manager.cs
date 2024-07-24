using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<EnemyMovement> enemies = new List<EnemyMovement>();
    [SerializeField] private PlayerMoneyManager playerMoneyManager;

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
        if (playerMoneyManager != null)
        {
            int currentMoney = playerMoneyManager.GetMoney();
            int upgradeCost = 100; // Define the cost for upgrading all enemies

            if (currentMoney >= upgradeCost)
            {
                playerMoneyManager.SetMoney(currentMoney - upgradeCost);
                PlayerPrefs.SetInt("EnemyDamage", enemies[0].damage + enemies[0].damageUpgradeAmount); // Assuming all enemies have the same upgrade amount
                PlayerPrefs.Save();

                foreach (var enemy in enemies)
                {
                    enemy.UpgradeDamage();
                }

                Debug.Log("All enemies upgraded.");
            }
            else
            {
                Debug.Log("Not enough money to upgrade all enemies' damage.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerMoneyManager not found.");
        }
    }

}
