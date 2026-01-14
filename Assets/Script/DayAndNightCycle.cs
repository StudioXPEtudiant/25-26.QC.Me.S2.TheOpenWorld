using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    [Range(0.1f, 60f)] 
    public float dayLengthInMinutes = 20f; // 20 minutes is a common 'long' game day
    
    [Header("Current Progress")]
    [Range(0f, 1f)]
    public float timeOfDay = 0f; // 0 is sunrise, 0.5 is sunset, 1 is next sunrise

    void Update()
    {
        // Calculate how much progress is made per second
        // (1 / total seconds in the day cycle)
        float progressPerSecond = 1f / (dayLengthInMinutes * 60f);

        // Update the timeOfDay value based on real time
        timeOfDay += progressPerSecond * Time.deltaTime;

        // Reset the day when it reaches 1
        if (timeOfDay >= 1f) 
        {
            timeOfDay = 0f;
        }

        // Apply rotation: 360 degrees * the percentage of the day passed
        // We subtract 90 so that 0.0 starts at "Morning" instead of "Noon"
        float sunRotation = (timeOfDay * 360f) - 90f;
        transform.localRotation = Quaternion.Euler(sunRotation, 0, 0);
    }
}