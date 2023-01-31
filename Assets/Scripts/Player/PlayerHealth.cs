using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;

    public event Action PlayerDeath;
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    public int MaxHealth 
    { 
        get { return maxHealth; }
    }

    private static PlayerHealth instance;

    public static PlayerHealth Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerHealth>();
            }
            return instance;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void Die()
    {
        PlayerDeath();
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}