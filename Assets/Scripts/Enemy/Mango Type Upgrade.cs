using UnityEngine;

public class MangoTypeUpgrade : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    //[SerializeField] private int tripleReticleCost = 800;
    [SerializeField] private PlayerScript player;
    [SerializeField] private GameObject playerMoney;
    private ShootProjectile shootProjectileScript;
    private PlayerMoneyManager playerMoneyManager;

    public void Start()
    {
        shootProjectileScript = projectile.GetComponent<ShootProjectile>();
        playerMoneyManager = playerMoney.GetComponent<PlayerMoneyManager>();
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


    public void BuyTripleReticle()
    {
        int initial = playerMoneyManager.GetMoney();
        if(initial >= 800)
        {
            playerMoneyManager.SetMoney(initial - 800);
            player.UnlockTripleReticle();
            Debug.Log("Triple Reticle purchased!");
        }
    }
}
