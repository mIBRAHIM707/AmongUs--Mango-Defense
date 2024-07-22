using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            Collider2D projectileCollider = GetComponent<Collider2D>();

            if (playerCollider != null && projectileCollider != null)
            {
                Physics2D.IgnoreCollision(playerCollider, projectileCollider);
            }
        }
    }
}
