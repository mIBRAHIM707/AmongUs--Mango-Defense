using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] public List<ReticleScript> reticles;
    [SerializeField] private AudioClip deselectSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float soundDelay = 0.5f;
    private int currentReticle = 0;
    private bool triple = false;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    private void Update()
    {
        if (triple) currentReticle = 1;
    }

    private void OnMouseDown()
    {
        Debug.Log("Player clicked: " + this.gameObject.name);
        reticles[currentReticle].Selected(this.gameObject);
    }

    private void OnMouseUp()
    {
        Debug.Log("Player released: " + this.gameObject.name);
        reticles[currentReticle].Deselect();

        StartCoroutine(PlaySoundWithDelay());
    }

    private IEnumerator PlaySoundWithDelay()
    {
        yield return new WaitForSeconds(soundDelay);
        if (deselectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deselectSound);
        }
    }
    public void UnlockTripleReticle()
    {
        triple = true;
    }
}
