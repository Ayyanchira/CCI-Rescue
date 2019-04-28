using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationAndMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField]private GameObject player;
    [SerializeField] Rigidbody rb;

    NavMeshAgent navMeshAgent;
    private float detect = 20f;
    private float attack = 1.3f;
    private float speed = 1f;

    private static PatrolMovement p;

    bool patrol = true;
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
        player = GameObject.FindGameObjectWithTag("Player");

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
                        if (startingWaypoint != null && Vector3.Distance(startingWaypoint.transform.position, transform.position) <= startingWaypoint.connectivityRadius)
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

    // Update is called once per frame
    void Update()
    {

        //if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, 1f)){
        //    Debug.DrawLine(transform.position, hit.point, Color.red);
        //    if (hit.collider.tag == "Player")
        //    {
                if (Vector3.Distance(player.transform.position, transform.position) <= detect)
                {
                    patrol = false;
                    Vector3 target = player.transform.position - transform.position;
                    if (target.magnitude <= attack)
                    {
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        animator.SetBool("isAttacking", true);
                        animator.SetBool("isWalking", false);

                    }
                    else
                    {
                        //target.Normalize();
                        //target *= speed;
                        //rb.velocity = target;
                        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(target), 0.1f);
                        SetDestination(player.transform.position);
                        animator.SetBool("isWalking", true);
                        animator.SetBool("isAttacking", false);

                    }

                }
                else
                {
                    patrol = true;
                    animator.SetBool("isWalking", true);

                }
                
                if (patrol)
                {
                    TravelPatrolPoints();
                }

    }
        //    else
        //    {
        //        patrol = true;
        //        animator.SetBool("isWalking", true);

        //    }
        //    if (patrol)
        //    {
        //        TravelPatrolPoints();
        //    }

        //}
        //else{
        //    patrol = true;
        //    animator.SetBool("isWalking", true);

        //}
        //if (patrol)
        //{
        //    TravelPatrolPoints();
        //}


    //}

    private void TravelPatrolPoints()
    {

        if (travel && navMeshAgent.remainingDistance <= 1.3f)
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

    private void SetDestination(Vector3 destination)
    {
        if(destination != null)
            navMeshAgent.SetDestination(destination);
    }

    private void SetDestination()
    {
        if (_waypointsVisited > 0)
        {
            ConnectedWaypoints nextWaypoint = _currentWaypoint.NextWayPoint(_previousWaypoint, gameObject);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWaypoint;
        }

        Vector3 targetVector = _currentWaypoint.transform.position;
        navMeshAgent.SetDestination(targetVector);
        travel = true;
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            //attack the player
            StartCoroutine(OnCompleteAttackAnimation(other));
            
        }
    }

    IEnumerator OnCompleteAttackAnimation(Collider player)
    {
        AnimatorClipInfo[] a = animator.GetCurrentAnimatorClipInfo(0);
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).nameHash);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < a[0].clip.length)
            yield return null;

        // TODO: Do something when animation did complete
        Debug.Log("Inside Player");
        player.gameObject.GetComponent<PlayerController>().KillPlayer();
        animator.SetBool("isAttacking", false);
        animator.SetBool("isWalking", true);
        patrol = true;

    }
}
