using UnityEngine;
using TMPro;

public class PlayerMoneyManager : MonoBehaviour
{
    private const string MoneyKey = "PlayerMoney";
    [SerializeField] public TextMeshProUGUI moneyText; 

    private void Start()
    {
        moneyText.text = "0";
        UpdateMoneyUI(GetMoney());
    }

    public void SetMoney(int amount)
    {
        PlayerPrefs.SetInt(MoneyKey, amount);
        PlayerPrefs.Save(); 
        Debug.Log("Money set to: " + amount);

        UpdateMoneyUI(amount);
    }

    public int GetMoney()
    {
        return PlayerPrefs.GetInt(MoneyKey, 0);
    }

    public void ResetMoney()
    {
        PlayerPrefs.DeleteKey(MoneyKey);
        PlayerPrefs.Save();
        Debug.Log("Money has been reset.");

        UpdateMoneyUI(0);
    }

    private void UpdateMoneyUI(int amount)
    {
        if (moneyText != null)
        {
            moneyText.text = "" + amount;
        }
    }
}
