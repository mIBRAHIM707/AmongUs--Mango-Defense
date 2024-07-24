using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] public ReticleScript reticleManager;
    [SerializeField] private AudioClip deselectSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float soundDelay = 0.5f; // Delay in seconds

    private void Start()
    {
        // Optionally check if AudioSource is assigned; if not, add it
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Player clicked: " + this.gameObject.name);
        reticleManager.Selected(this.gameObject);
    }

    private void OnMouseUp()
    {
        Debug.Log("Player released: " + this.gameObject.name);
        reticleManager.Deselect();

        // Start the coroutine to play the sound after a delay
        StartCoroutine(PlaySoundWithDelay());
    }

    private IEnumerator PlaySoundWithDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(soundDelay);

        // Play the sound
        if (deselectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deselectSound);
        }
    }
}
