using UnityEngine;
using TMPro; // If using TextMeshPro

public class PlayerMoneyManager : MonoBehaviour
{
    private const string MoneyKey = "PlayerMoney";
    [SerializeField] public TextMeshProUGUI moneyText; // Reference to the TextMeshProUGUI component

    private void Start()
    {
        // Update the UI text with the initial money value
        UpdateMoneyUI(GetMoney());
    }

    // Method to set the player's money
    public void SetMoney(int amount)
    {
        PlayerPrefs.SetInt(MoneyKey, amount);
        PlayerPrefs.Save(); // Ensure the data is saved
        Debug.Log("Money set to: " + amount);

        // Update the UI
        UpdateMoneyUI(amount);
    }

    // Method to get the player's money
    public int GetMoney()
    {
        return PlayerPrefs.GetInt(MoneyKey, 0); // Default to 0 if no money is saved
    }

    // Method to reset the player's money
    public void ResetMoney()
    {
        PlayerPrefs.DeleteKey(MoneyKey);
        PlayerPrefs.Save();
        Debug.Log("Money has been reset.");

        // Update the UI
        UpdateMoneyUI(0);
    }

    // Method to update the UI text
    private void UpdateMoneyUI(int amount)
    {
        if (moneyText != null)
        {
            moneyText.text = "" + amount;
        }
    }
}
