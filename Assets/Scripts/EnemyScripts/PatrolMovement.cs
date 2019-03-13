using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PatrolMovement : MonoBehaviour
{

    //[SerializeField] Transform destination;
    NavMeshAgent navMeshAgent;
    ConnectedWaypoints _currentWaypoint;
    ConnectedWaypoints _previousWaypoint;
    int _waypointsVisited;


   // [SerializeField] List<Waypoints> patrolPoints;
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

        if (navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            if (_currentWaypoint == null)
            {
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("PatrolPoint");

                if (allWaypoints.Length > 0)
                {
                    while (_currentWaypoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                        ConnectedWaypoints startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoints>();

                        //i.e. we found a waypoint.
                        if (startingWaypoint != null)
                        {
                            _currentWaypoint = startingWaypoint;
                        }
                    }
                }
                else
                {
                    Debug.LogError("Failed to find any waypoints for use in the scene.");
                }
            }

            SetDestination();
        }
    }

    private void SetDestination()
    {
        if (_waypointsVisited > 0)
        {
            ConnectedWaypoints nextWaypoint = _currentWaypoint.NextWayPoint(_previousWaypoint);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWaypoint;
        }

        Vector3 targetVector = _currentWaypoint.transform.position;
        navMeshAgent.SetDestination(targetVector);
        travel = true;
    }


    // Update is called once per frame
    public void Patrol()
    {
        if (travel && navMeshAgent.remainingDistance <= 1.0f)
        {
            travel = false;
            _waypointsVisited++;

            //If we're going to wait, then wait.
            if (wait)
            {
                wait = true;
                waitTimer = 0f;
            }
            else
            {
                SetDestination();
            }
        }

        //Instead if we're waiting.
        if (wait)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                wait = false;

                SetDestination();
            }
        }
    }

    //private void ChangePatrolPoint()
    //{
    //    if(UnityEngine.Random.Range(0f, 1f) <= switchProbability)
    //    {
    //        patrolForward = !patrolForward;

    //    }
    //    if (patrolForward)
    //    {
    //        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;

    //    }
    //    else
    //    {
    //        if(--currentPatrolIndex < 0)
    //        {
    //            currentPatrolIndex = patrolPoints.Count - 1;
    //        }
    //    }
    //}
}
