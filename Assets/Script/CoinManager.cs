using UnityEngine;
using TMPro; // N�cessaire pour l'affichage de texte

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int coinCount = 0;
    public TextMeshProUGUI coinText; // Glisse ton texte UI ici

    void Awake()
    {
        // Syst�me pour s'assurer qu'il n'y a qu'un seul Manager
        if (instance == null) instance = this;
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        coinText.text = "Coins : " + coinCount.ToString();
    }
}