using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Image healthBar;
    [SerializeField] public int damage;
    [SerializeField] public int damageUpgradeAmount;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float fillSpeed = 0.5f;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private PlayerMoneyManager playerMoneyManager;
<<<<<<< HEAD
    private int _MoneyAmount = 50;
=======
    [SerializeField] private PlayerLivesManager playerLivesManager;
    private int currentHealth;
>>>>>>> 5c60f80c6420129838409a2da668838a2c13d4b1

    private int currentHealth;
    
    private Tween healthBarFillTween;
    private Tween healthBarColorTween;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (PlayerPrefs.HasKey("EnemyDamage"))
        {
            this.damage = PlayerPrefs.GetInt("EnemyDamage");
        }

        if (playerMoneyManager == null)
        {
            playerMoneyManager = FindObjectOfType<PlayerMoneyManager>();
        }
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < -8.81)
        {
            Destroy(gameObject);

            if (playerLivesManager != null)
            {
                playerLivesManager.DeductLife(); 
                Debug.Log("Life deducted. Remaining lives: " + playerLivesManager.GetLives()); 
            }
            else
            {
                Debug.LogWarning("PlayerLivesManager not found.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(damage);
            Destroy(collision.gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBarFillTween?.Kill();
            healthBarFillTween = healthBar.DOFillAmount(healthPercentage, fillSpeed).SetEase(Ease.Linear);

            healthBarColorTween?.Kill();
            healthBarColorTween = healthBar.DOColor(colorGradient.Evaluate(healthPercentage), fillSpeed).SetEase(Ease.Linear);
        }
        else
        {
            Debug.LogWarning("HealthBar Image is not assigned in the Inspector.");
        }
    }

    private void TakeDamage(int damage)
    {
        int previousHealth = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (previousHealth != currentHealth)
        {
            UpdateHealthBar();
        }

        Debug.Log("Damage taken. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            AddMoney(_MoneyAmount);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        healthBarFillTween?.Kill();
        healthBarColorTween?.Kill();
    }

    public void UpgradeDamage()
    {
        if (playerMoneyManager != null)
        {
            int currentMoney = playerMoneyManager.GetMoney();
            if (currentMoney >= 100)
            {
                this.damage += damageUpgradeAmount;
                playerMoneyManager.SetMoney(currentMoney - 100);
                PlayerPrefs.SetInt("EnemyDamage", this.damage); 
                PlayerPrefs.Save();
                Debug.Log("Damage Upgraded to: " + this.damage);
            }
            else
            {
                Debug.Log("Not enough money to upgrade damage.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerMoneyManager not found.");
        }
    }

    public void AddMoney(int amount)
    {
        if (playerMoneyManager != null)
        {
            int currentMoney = playerMoneyManager.GetMoney();
            playerMoneyManager.SetMoney(currentMoney + amount);
        }
        else
        {
            Debug.LogWarning("PlayerMoneyManager not found.");
        }
    }

    public void SubtractMoney(int amount)
    {
        if (playerMoneyManager != null)
        {
            int currentMoney = playerMoneyManager.GetMoney();
            playerMoneyManager.SetMoney(currentMoney - amount);
        }
        else
        {
            Debug.LogWarning("PlayerMoneyManager not found.");
        }
    }
}
