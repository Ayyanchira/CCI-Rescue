using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{

    //[SerializeField] Transform destination;
    NavMeshAgent navMeshAgent;


    [SerializeField] List<Waypoints> patrolPoints;
    [SerializeField] bool patrolWaiting;
    [SerializeField] float totalWaitTime = 3f;
    [SerializeField] float switchProbability = .2f;

    int currentPatrolIndex;
    bool travel = true;
    bool wait;
    bool patrolForward;
    float waitTimer;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(navMeshAgent == null)
        {
            Debug.Log("error");
        }
        else
        {
            if (patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points for basic patrolling behaviour.");
            }

        }
    }

    private void SetDestination()
    {
        if(patrolPoints != null)
        {
            Vector3 target = patrolPoints[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(target);
            travel = true;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log(currentPatrolIndex);
        if(travel && navMeshAgent.remainingDistance <= 1.0f)
        {
            travel = false;
            if (patrolWaiting)
            {
                wait = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }
        if (wait)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= totalWaitTime)
            {
                wait = false;
                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f, 1f) <= switchProbability)
        {
            patrolForward = !patrolForward;

        }
        if (patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;

        }
        else
        {
            if(--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }
}
