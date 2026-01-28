using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [Tooltip("Amount of HP to remove per hit")]
    public float damageAmount = 10f; 

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has the PlayerHealth script
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            // This calls the function and passes '10'
            health.TakeDamage(damageAmount);
        }
    }
}