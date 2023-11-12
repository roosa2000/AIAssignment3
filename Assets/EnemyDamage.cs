using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
        
    }

    private void CheckCollision(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Player"){

            playerHealth.TakeDamage(damage);
        }
    }

}