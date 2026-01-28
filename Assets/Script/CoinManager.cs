using UnityEngine;
using TMPro; // Nécessaire pour l'affichage de texte

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int coinCount = 0;
    public TextMeshProUGUI coinText; // Glisse ton texte UI ici

    void Awake()
    {
        // Système pour s'assurer qu'il n'y a qu'un seul Manager
        if (instance == null) instance = this;
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        coinText.text = "Pièces : " + coinCount.ToString();
    }
}