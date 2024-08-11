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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

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
