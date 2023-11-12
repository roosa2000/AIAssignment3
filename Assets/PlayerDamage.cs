using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDamage : MonoBehaviour
{
    public int damage;
    private float pushForce = 1f;
    Unit unit;
    public float speed;
    [SerializeField]private GameObject enemy;
    public void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }

    private void CheckCollision(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                PushPlayer(collision.transform);
            }
        }
    }

    public void PushPlayer(Transform enemyTransform)
    {
        GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        Vector2 pushDirection = (enemyTransform.position - transform.position).normalized;

        // Apply force to the player
        GetComponent<Rigidbody2D>().AddForce(-pushDirection * pushForce, ForceMode2D.Impulse);
        float targetRange = 2f;

        if (Vector3.Distance(transform.position, enemyTransform.position) < targetRange)
        {
            // Move towards the enemy only if it's within the target range
            transform.position = Vector2.MoveTowards(transform.position, enemyTransform.position, speed * Time.deltaTime);
        }
    }
}