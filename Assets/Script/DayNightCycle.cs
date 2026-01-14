using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Time in minutes for a full day/night cycle")]
    public float dayLengthInMinutes = 1f; 
    
    [SerializeField] private Light sun;

    void Update()
    {
        // Calculate how many degrees the sun should move per second
        // 360 degrees / (minutes * 60 seconds)
        float rotationSpeed = 360f / (dayLengthInMinutes * 60f);

        // Rotate the light around the X-axis
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
