using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private List<GameObject> projectiles; // List to hold different projectile prefabs
    [SerializeField] private float speed;
    private GameObject currentProjectile; // Currently selected projectile

   

    void Start()
    {
        // Default to the first projectile in the list
        if (projectiles.Count > 0)
        {
            currentProjectile = projectiles[0];
        }
        else
        {
            Debug.LogError("No projectiles assigned in the list.");
        }
    }

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(currentProjectile, null);
        projectile.transform.position = this.transform.position;
        projectile.GetComponent<Rigidbody2D>().AddRelativeForce(this.transform.right * speed);
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
}


