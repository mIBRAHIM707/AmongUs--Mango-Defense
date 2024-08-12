using UnityEngine;

public class EnemySpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] animationSprites;
    [SerializeField] private float animationSpeed = 0.1f;
    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex;
    private float timer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= animationSpeed)
        {
            timer = 0f;
            currentSpriteIndex++;

            if (currentSpriteIndex >= animationSprites.Length)
            {
                currentSpriteIndex = 0;
            }

            spriteRenderer.sprite = animationSprites[currentSpriteIndex];
        }
    }
}
