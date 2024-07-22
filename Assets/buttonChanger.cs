using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite clickedSprite;
    private Image buttonImage;
    private Button button;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeSprite);
    }

    private void ChangeSprite()
    {
        buttonImage.sprite = clickedSprite;
    }

    public void ResetSprite()
    {
        buttonImage.sprite = defaultSprite;
    }
}
