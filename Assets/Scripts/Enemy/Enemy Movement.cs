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
    private int currentHealth;

    private Tween healthBarFillTween;
    private Tween healthBarColorTween;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < -13)
        {
            Destroy(gameObject);
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
        this.damage += damageUpgradeAmount;
        Debug.Log("Damage Upgraded");
    }

}
