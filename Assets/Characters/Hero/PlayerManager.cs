using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public static PlayerManager instance;

    public Rigidbody rb;

    public float speed;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {

        switch (Input.inputString){
            case "w":
                animator.applyRootMotion = false;
                rb.AddForce(transform.forward * speed);
                animator.SetBool("isRunning", true);
                break;

            case "s":
                animator.applyRootMotion = false;
                animator.SetBool("isMovingBack", true);
                break;

            case "a":
                animator.applyRootMotion = false;
                animator.SetBool("isStrafingLeft", true);
                break;

            case "d":
                animator.applyRootMotion = false;
                animator.SetBool("isStrafingRight", true);
                break;

            default:
                SetAnimationToIdle();
                break;


        }
       
        ////Check for Run
        //if (Input.GetKey(KeyCode.W))
        //{
        //    animator.applyRootMotion = false;
        //    rb.AddForce(transform.forward * speed);
        //    animator.SetBool("isRunning", true);
        //}else{
        //    SetAnimationToIdle();
        //}
        

        ////Check for Back
        //if (Input.GetKey(KeyCode.S))
        //{
        //    animator.SetBool("isMovingBack", true);
        //}
        //else
        //{
        //    SetAnimationToIdle();
        //}



        ////Check for Strafe Right
        //if (Input.GetKey(KeyCode.D))
        //{
        //    animator.SetBool("isStrafingRight", true);
        //}
        //else
        //{
        //    SetAnimationToIdle();
        //}



        ////Check for Strafe Left
        //if (Input.GetKey(KeyCode.A))
        //{
        //    animator.SetBool("isStrafingLeft", true);
        //}
        //else
        //{
        //    SetAnimationToIdle();
        //}


    }

    private void SetAnimationToIdle()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isStrafingRight", false);
        animator.SetBool("isStrafingLeft", false);
        animator.SetBool("isMovingBack", false);
        animator.applyRootMotion = true;
    }
}
