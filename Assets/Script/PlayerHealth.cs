using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider healthSlider;

    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Regeneration Settings")]
    public bool canRegenerate = true;
    public float regenDelay = 5f; 
    public float regenRate = 5f;  

    [Header("Respawn Coordinates")]
    public Vector3 respawnCoordinates = new Vector3(542.624f, 22.62f, 775f);

    private CharacterController controller;
    private Coroutine regenCoroutine;

    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();
        
        // Setup Slider
        if (healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        // Subtract EXACTLY the amount passed (10)
        currentHealth -= amount;
        
        // Safety: don't go below 0
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log("Took " + amount + " damage. Health is now: " + currentHealth);

        UpdateUI();

        // Restart Regeneration Timer
        if (regenCoroutine != null) StopCoroutine(regenCoroutine);

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (canRegenerate)
        {
            regenCoroutine = StartCoroutine(RegenHealthRoutine());
        }
    }

    IEnumerator RegenHealthRoutine()
    {
        yield return new WaitForSeconds(regenDelay);

        while (currentHealth < maxHealth)
        {
            currentHealth += regenRate * Time.deltaTime;
            // Don't go over 100
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            
            UpdateUI();
            yield return null;
        }
    }

    void UpdateUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    void Die()
    {
        currentHealth = maxHealth; // Reset to 100
        UpdateUI();
        TeleportPlayer();
    }

    void TeleportPlayer()
    {
        if (controller != null)
        {
            controller.enabled = false;
            transform.position = respawnCoordinates;
            controller.enabled = true;
        }
        else
        {
            transform.position = respawnCoordinates;
        }
    }
}