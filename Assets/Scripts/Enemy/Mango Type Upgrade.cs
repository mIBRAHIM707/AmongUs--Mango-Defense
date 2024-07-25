using UnityEngine;

public class MangoTypeUpgrade : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    private ShootProjectile shootProjectileScript;

    public void Start()
    {
        shootProjectileScript = projectile.GetComponent<ShootProjectile>();
    }
    public void OnBuyNewProjectileButtonClicked()
    {
        if (shootProjectileScript != null)
        {
            shootProjectileScript.SwitchProjectile(1);
        }
        else
        {
            Debug.LogWarning("ShootProjectile script reference is missing.");
        }
    }
}
