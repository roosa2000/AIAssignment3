using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    private Vector3 startingPosition;
    public Transform target;

    private enum State{
        MoveToTarget,
        AttackEnemy
    }
    private State state;
    Unit unit;
    private Vector2 currentTargetPosition;

    private GameObject[] enemies;

    public void Awake()
    {
        state = State.MoveToTarget;
        unit = GetComponent<Unit>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 1.5f)
        {
            Time.timeScale = 0.0f;
            return;
        }

        switch (state)
        {
            default:
            case State.MoveToTarget:
                if (!currentTargetPosition.Equals(target.position))
                {
                    currentTargetPosition = target.position;
                    unit.MoveTo(currentTargetPosition);
                }
                
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies != null && enemies.Length > 0)
                {
                    FindEnemy();
                }
                break;

            case State.AttackEnemy:
                if (enemies != null && enemies.Length > 0)
                {
                    GameObject closestEnemy = FindEnemy();
                    if (closestEnemy != null)
                    {
                        if (!currentTargetPosition.Equals(closestEnemy.transform.position))
                        {
                            currentTargetPosition = closestEnemy.transform.position;
                            unit.MoveTo(currentTargetPosition);
                        }
                    }
                }
                break;
        }


    }

    private GameObject FindEnemy()
    {
        float targetRange = 10f;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance && distance < targetRange)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        if (closestEnemy != null)
        {
            state = State.AttackEnemy;
        }
        else{
            state = State.MoveToTarget;
        }

        return closestEnemy;
    }
}
