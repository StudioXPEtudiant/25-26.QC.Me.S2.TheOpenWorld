using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;
    public float rotationSpeed = 100f; // Vitesse de rotation

    void Update()
    {
        // Fait tourner la pièce sur elle-même chaque seconde
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            // Ajoute les points au manager
            CoinManager.instance.AddCoins(value); 
            
            // Détruit la pièce
            Destroy(gameObject); 
        }
    }
}