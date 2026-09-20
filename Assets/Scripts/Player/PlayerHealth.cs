using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public event Action<float> OnHealthChanged;

    private readonly float maxHealth = 100f;
    private float currentHealth;

    private void Awake() {
        currentHealth = maxHealth;
    }

    private void TakeDamage(float damageAmount) {
        currentHealth -= damageAmount;
        if (currentHealth <= 0) HandleDeath();
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void HandleHeal(float healAmount) {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void HandleDeath() {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }
}