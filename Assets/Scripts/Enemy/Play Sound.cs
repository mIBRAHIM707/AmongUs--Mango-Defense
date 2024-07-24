using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound; // Sound to play when hit
    [SerializeField] private float soundVolume = 1.0f; // Volume of the sound

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        // Ensure there is an AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.spatialBlend = 1.0f; // Fully 3D sound
        audioSource.volume = soundVolume;
    }

    public void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound, soundVolume);
        }
        else
        {
            Debug.LogWarning("Hit sound or AudioSource is not assigned.");
        }
    }
}
