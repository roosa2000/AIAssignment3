using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int health;
    [SerializeField] PlayerHealthBar playerHealthBar;
    // Start is called before the first frame update

    public void Awake()
    {
        playerHealthBar = GetComponentInChildren<PlayerHealthBar>();
    }

    void Start()
    {
        health = maxHealth;
        playerHealthBar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        playerHealthBar.UpdateHealthBar(health, maxHealth);
        if(health <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
