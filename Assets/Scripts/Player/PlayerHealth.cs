using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private int maxHealth = 100;
        private int currentHealth;

        public event Action PlayerDeath;
        public int CurrentHealth => currentHealth;

        public int MaxHealth => maxHealth;

        private static PlayerHealth _instance;

        public static PlayerHealth Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PlayerHealth>();
                }
                return _instance;
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
}