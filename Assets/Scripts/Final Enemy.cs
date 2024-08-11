using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using FirstGearGames.SmoothCameraShaker;

public class FinalEnemy : MonoBehaviour
{

    //public ShakeData explosionShakeData;

    [SerializeField] private float speed;
    [SerializeField] private Image healthBar;
    [SerializeField] public int damage;
    [SerializeField] public int damageUpgradeAmount;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float fillSpeed = 0.5f;
    [SerializeField] private Gradient colorGradient;
    [SerializeField] private PlayerMoneyManager playerMoneyManager;
    [SerializeField] private PlayerLivesManager playerLivesManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject vignette;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float soundVolume = 1.0f;
    [SerializeField] private AudioClip castlerHit;


    private CameraShake cameraShake;
    private PlaySound enemyBody;
    private int _MoneyAmount = 50;
    private int currentHealth;
    private Tween healthBarFillTween;
    private Tween healthBarColorTween;
    private bool isDead = false;
    private float directionChangeCooldown = 0.1f;
    private float timeSinceLastDirectionChange = 0f;
    private float ySpeed = -1f;


    public UnityEvent OnDied;
    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
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
        timeSinceLastDirectionChange += Time.deltaTime;

        Vector3 newPosition = transform.position + new Vector3(speed * Time.deltaTime, ySpeed * Time.deltaTime, 0);

        if (timeSinceLastDirectionChange > directionChangeCooldown && (newPosition.x > -2.2f || newPosition.x < -6.2f))
        {
            speed = -speed;
            timeSinceLastDirectionChange = 0f; // Reset the cooldown timer
        }
        transform.position = newPosition;


        if (transform.position.y < -7.8f)
        {
            vignette.SetActive(true);
        }

        if (transform.position.y < -8.81f)
        {
            AudioSource.PlayClipAtPoint(castlerHit, transform.position, soundVolume);
            cameraShake.Shake();
            Destroy(gameObject);
            vignette.SetActive(false);

            if (playerLivesManager != null)
            {
                playerLivesManager.DeductLife();
                Debug.Log("Life deducted. Remaining lives: " + playerLivesManager.GetLives());
            }

            if (playerLivesManager.GetLives() > 1)
            {
                enemyManager.OnEnemyDefeated();
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
        if (this.transform.position.y < -1.5)
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


                if (!isDead) FindObjectOfType<EnemyManager>().OnEnemyDefeated();
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
