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
        Debug.Log(remainingEnemies);
    }

    public void OnEnemyDefeated()
    {
        remainingEnemies--;
        Debug.Log(remainingEnemies);
        if (remainingEnemies <= 0)
        {
            levelManager.CompleteLevel();
        }
    }
}
