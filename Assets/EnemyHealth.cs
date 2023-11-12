using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public int maxHealth = 10;
    public int health;
    [SerializeField] HealthBar healthBar;

    public void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }
    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
