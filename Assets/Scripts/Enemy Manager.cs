using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TMP_Text enemiesLeftText;
    private int totalEnemies;
    private int remainingEnemies;

    void Start()
    {
        totalEnemies = FindObjectsOfType<EnemyMovement>().Length;
        remainingEnemies = totalEnemies;
        Debug.Log(remainingEnemies);
        UpdateEnemiesLeftUI(remainingEnemies, totalEnemies);
    }

    public void OnEnemyDefeated()
    {
        remainingEnemies--;
        UpdateEnemiesLeftUI(remainingEnemies, totalEnemies);
        Debug.Log(remainingEnemies);    
        if (remainingEnemies <= 0)
        {
            levelManager.CompleteLevel();
        }
    }
    public void UpdateEnemiesLeftUI(int remainingEnemies, int totalEnemies)
    {
        float fillAmount = (float)remainingEnemies / totalEnemies;

        healthBarFill.DOFillAmount(fillAmount, 1.0f).SetEase(Ease.InOutQuad);

        enemiesLeftText.DOFade(0, 0.25f).OnComplete(() =>
        {
            enemiesLeftText.text = "Enemies Left: " + remainingEnemies;
            enemiesLeftText.DOFade(1, 0.25f);
        });
    }
}
