using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager; 
    private int totalEnemies;
    private int remainingEnemies;

    void Start()
    {
        totalEnemies = FindObjectsOfType<EnemyMovement>().Length;
        remainingEnemies = totalEnemies;
    }

    public void OnEnemyDefeated()
    {
        remainingEnemies--;
        if (remainingEnemies <= 0)
        {
            levelManager.CompleteLevel();
        }
    }
}
