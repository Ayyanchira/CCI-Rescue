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

       
        //Check for Run
        if (Input.GetKey(KeyCode.W))
        {
            //rb.AddForce(transform.forward * speed);
            animator.SetBool("isRunning", true);
        }else{
            animator.SetBool("isRunning", false);
        }
        

        //Check for Back
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isMovingBack", true);
        }
        else
        {
            animator.SetBool("isMovingBack", false);

        }



        //Check for Strafe Right
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isStrafingRight", true);
        }
        else
        {
            animator.SetBool("isStrafingRight", false);
        }



        //Check for Strafe Left
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isStrafingLeft", true);
        }
        else
        {
            animator.SetBool("isStrafingLeft", false);
        }

        //if (Input.GetAxis("Mouse X") < 0){

        //    transform.Rotate(Vector3.up * -speed); 
        //}
           
        //if (Input.GetAxis("Mouse X") > 0)
            //transform.Rotate(Vector3.up * speed);

    }
}
