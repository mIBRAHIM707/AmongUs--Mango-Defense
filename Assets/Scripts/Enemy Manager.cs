using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager; // Reference to the LevelManager script
    private int totalEnemies;
    private int remainingEnemies;

    void Start()
    {
        // Initialize total and remaining enemies count
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
