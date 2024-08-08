using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using FirstGearGames.SmoothCameraShaker;

public class EnemyMovement : MonoBehaviour
{

    public ShakeData explosionShakeData;

    [SerializeField] private float speed;
    [SerializeField] private Image healthBar;
    [SerializeField] public int damage;
    [SerializeField] public int damageUpgradeAmount;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float fillSpeed = 0.5f;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private PlayerMoneyManager playerMoneyManager;
    [SerializeField] private PlayerLivesManager playerLivesManager;
    [SerializeField] private GameObject vignette;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float soundVolume = 1.0f;
    [SerializeField] private AudioClip castlerHit;




    private PlaySound enemyBody;
    private int _MoneyAmount = 50;
    private int currentHealth;
    private Tween healthBarFillTween;
    private Tween healthBarColorTween;
    private bool isDead = false;


    public UnityEvent OnDied;
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

        enemyBody = GetComponentInChildren<PlaySound>();
        if (enemyBody == null)
        {
            Debug.LogWarning("EnemyBody component not found on child GameObject.");
        }
    }

    void Update()
    {

        transform.position += Vector3.down * speed * Time.deltaTime;

        if(transform.position.y < -7.8)
        {
            vignette.SetActive(true);
            Debug.Log("VIGNETTE TRUE");
        }

        if (transform.position.y < -8.81)
        {
            AudioSource.PlayClipAtPoint(castlerHit, transform.position, soundVolume);
            CameraShakerHandler.Shake(explosionShakeData);
            Destroy(gameObject);
            vignette.SetActive(false);

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
        //if (collision.gameObject.CompareTag("Projectile"))
        //{
            Debug.Log("Triggered");
            TakeDamage(damage);
            Destroy(collision.gameObject);
        //}

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
        if(this.transform.position.y < -1.5)
        {
            int previousHealth = currentHealth;
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (previousHealth != currentHealth)
            {
                UpdateHealthBar();

                if (enemyBody != null)
                {
                    enemyBody.PlayHitSound();
                }
            }

            Debug.Log("Damage taken. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
               

                if(!isDead) FindObjectOfType<EnemyManager>().OnEnemyDefeated();
                isDead = true;
                AddMoney(_MoneyAmount);

                if (deathSound != null)
                {
                    AudioSource.PlayClipAtPoint(deathSound, transform.position, soundVolume);
                }
                OnDied.Invoke();  //FOR ANIMATION
                Destroy(gameObject, 1f);
            }
        }
    }

    private void OnDestroy()
    {
        healthBarFillTween?.Kill();
        healthBarColorTween?.Kill();
    }

    public void UpgradeDamage()
    {
        this.damage += damageUpgradeAmount;
        Debug.Log("Damage Upgraded to: " + this.damage);
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
