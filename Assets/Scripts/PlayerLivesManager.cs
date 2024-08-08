using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerLivesManager : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    [SerializeField] private GameObject gameOverPanel;
    public TextMeshProUGUI livesText;
    public float gameOverDelay = 1f; // Time in seconds before showing the game over panel

    private void Start()
    {
        UpdateLivesUI();
    }

    public void DeductLife()
    {
        lives--;
        Debug.Log("Life deducted. Remaining lives: " + lives); // Debug log to verify life deduction
        UpdateLivesUI();

        if (lives <= 0)
        {
            StartCoroutine(GameOverAfterDelay());
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    private IEnumerator GameOverAfterDelay()
    {
        Debug.Log("Game Over!");
        yield return new WaitForSeconds(gameOverDelay);
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverPanel not assigned.");
        }

        Time.timeScale = 0f; // This will pause the game
    }

    public int GetLives()
    {
        return lives;
    }
}
