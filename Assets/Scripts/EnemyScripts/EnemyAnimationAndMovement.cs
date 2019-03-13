using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationAndMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform player;
    [SerializeField] Rigidbody rb;

    NavMeshAgent navMeshAgent;
    private float detect = 10f;
    private float attack = 2f;
    private float speed = 1f;

    bool patrol = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= detect)
        {
            patrol = false;
            Vector3 target = player.transform.position - transform.position;
            if(target.magnitude <= attack)
            {
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
            
        }
        if (patrol)
        {
            TravelPatrolPoints();
        }
    }

    private void TravelPatrolPoints()
    {
        
    }

    private void SetDestination(Vector3 destination)
    {
        if(destination != null)
            navMeshAgent.SetDestination(destination);
    }
}
