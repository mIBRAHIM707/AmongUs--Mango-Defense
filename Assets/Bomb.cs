using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5.0f; // Radius of the explosion
    public LayerMask enemyLayer; // Layer mask to filter out enemies

    private bool hasExploded = false;

    //BombKillEnemy killer;
    private void Start()
    {
        // Start the coroutine to destroy the bomb after 5 seconds
        StartCoroutine(DestroyAfterDelay(5f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("mar ja ch");
            Destroy(collision.gameObject);
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Draw the explosion radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        // Destroy the bomb game object
        Destroy(gameObject);
    }
}
