using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletePanel;
    private bool levelCompleted = false;

    void Start()
    {
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }
    }

    public void CompleteLevel()
    {
        if (!levelCompleted)
        {
            levelCompleted = true;
            if (levelCompletePanel != null)
            {
                levelCompletePanel.SetActive(true);
            }
        }
    }

    public void OnNextLevelButtonClicked()
    {
        // Load the next level (make sure to set up the build settings with the correct scene indices)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Optionally, you could check if the next scene exists
        if (Application.CanStreamedLevelBeLoaded(nextSceneIndex))
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }
}
