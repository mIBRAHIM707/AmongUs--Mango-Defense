using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private List<GameObject> projectiles;
    [SerializeField] private float baseSpeed;
    private GameObject currentProjectile;

    void Start()
    {
        if (projectiles.Count > 0)
        {
            currentProjectile = projectiles[0];
        }
        else
        {
            Debug.LogError("No projectiles assigned in the list.");
        }
    }

    public void FireProjectile(float speedMultiplier)
    {
        GameObject projectile = Instantiate(currentProjectile, null);
        projectile.transform.position = this.transform.position;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = -this.transform.right * baseSpeed * speedMultiplier;
            StartCoroutine(CheckVelocityAndDestroy(projectile, rb));
        }
        else
        {
            Debug.LogError("Rigidbody2D component not found on the projectile.");
        }
    }

    public void SwitchProjectile(int index)
    {
        if (index >= 0 && index < projectiles.Count)
        {
            currentProjectile = projectiles[index];
            Debug.Log("Switched to projectile: " + currentProjectile.name);
        }
        else
        {
            Debug.LogWarning("Invalid projectile index: " + index);
        }
    }

    private IEnumerator CheckVelocityAndDestroy(GameObject projectile, Rigidbody2D rb)
    {
        while (rb.velocity.magnitude > 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(projectile);
    }
}


