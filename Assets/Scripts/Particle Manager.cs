using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticleSystem;
    private ParticleSystem.EmissionModule emissionModule;

    void Start()
    {
        if (walkParticleSystem == null)
        {
            Debug.LogError("Walking particle system is not assigned.");
        }
        else
        {
            emissionModule = walkParticleSystem.emission;
            emissionModule.enabled = false;
        }
    }

    public void StartWalkingParticles()
    {
        if (walkParticleSystem == null) return;

        emissionModule.enabled = true; 
        walkParticleSystem.Play();
    }

    public void StopWalkingParticles()
    {
        if (walkParticleSystem == null) return;

        emissionModule.enabled = false; 
        walkParticleSystem.Stop();
    }
}
