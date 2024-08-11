using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerLivesManager : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    [SerializeField] private GameObject gameOverPanel;
    public TextMeshProUGUI livesText;

    private void Start()
    {
        UpdateLivesUI();
    }

    public void DeductLife()
    {
        this.lives--;
        Debug.Log("Life deducted. Remaining lives: " + lives); // Debug log to verify life deduction
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
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
        return this.lives;
    }


}
