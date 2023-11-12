using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Vector3 playerPosition;
    Unit unit;
    private enum State{
        Roaming,
        ChasePlayer,
    }
    private State state;

    private void Awake(){
        unit = GetComponent<Unit>();
        state = State.Roaming;
    }

    private void Start(){
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update(){
        GameObject obj = GameObject.Find("Player");

        if (obj != null)
        {
            playerPosition = obj.transform.position;
        }
        switch(state){
            default:
            case State.Roaming:
                unit.MoveTo(roamPosition);
                float reachedPositionDistance = 1f;

                if(Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                {
                    roamPosition = GetRoamingPosition();
                }
                
                FindTarget();
                break;
            case State.ChasePlayer:
                unit.MoveTo(playerPosition);
                break;
        }

        

        
    }

    private Vector3 GetRoamingPosition(){
        return startingPosition + GetRandomDir() * Random.Range(10f, 50f);
    }

    public static Vector3 GetRandomDir(){
        return new Vector3(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private void FindTarget(){
        float targetRange = 50f;
        if(Vector3.Distance(transform.position, playerPosition)<targetRange){
            state = State.ChasePlayer;
        }
    }
}
