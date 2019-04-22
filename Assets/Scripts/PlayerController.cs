using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float moveSpeed;
    [SerializeField] private float runMultiplier;
    private CharacterController character;
    private Vector3 movement;
    [SerializeField] private float rotateSpeed;
    //static //animator //anim;

    // Use this for initialization
    void Start ()
    {
        character = GetComponent<CharacterController>();
        //anim = GetComponent<//animator>();
    }
	
	// Update is called once per frame
    // *** UPDATE CHANGED FROM LATEUPDATE to FIXEDUPDATE to sync with CAMERACONTROLLER
	void FixedUpdate ()
    {
        ////Basic attack
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    //anim.SetBool("isAttacking", true);
        //    /**/moveSpeed = 0.0f;
        //}
        //else
        //{
        //    //anim.SetBool("isAttacking", false);
        //    /**/moveSpeed = 6.0f;
        //}

        ////Heavy attack
        //if (Input.GetKey(KeyCode.Mouse1))
        //{
        //    //anim.SetBool("HeavyAttack", true);
        //    /**/moveSpeed = 0.0f;
        //}
        //else
        //{
        //    //anim.SetBool("HeavyAttack", false);
        //    /**/moveSpeed = 6.0f;
        //}

        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        transform.Rotate(0, horizontal, 0);
        //Move forward
        if (Input.GetKey(KeyCode.W))
        {
            ////anim.SetBool("isRunning", true);
            movement = (transform.forward * Input.GetAxis("Vertical"));
            movement = movement.normalized * moveSpeed;
            character.Move(movement * Time.deltaTime);
            if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                ////anim.SetBool("isRunning", true);
                movement = (transform.forward * Input.GetAxis("Vertical"));
                movement = movement.normalized * (moveSpeed * runMultiplier);
                character.Move(movement * Time.deltaTime);
            }
        }
        else
        {
            ////anim.SetBool("isRunning", false);
        }

        //Move back
        if (Input.GetKey(KeyCode.S))
        {
            //anim.SetBool("runBack", true);
            movement = (transform.forward * Input.GetAxis("Vertical"));
            movement = movement.normalized * moveSpeed;
            character.Move(movement * Time.deltaTime);
        }
        else
        {
            //anim.SetBool("runBack", false);
        }

        //Move left
        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)))
        {
            //anim.SetBool("runLeft", true);
            movement = (transform.right * Input.GetAxis("Horizontal"));
            movement = movement.normalized * moveSpeed;
            character.Move(movement * Time.deltaTime);
        }
        else
        {
            //anim.SetBool("runLeft", false);
        }

        //Move right
        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)))
        {
            //anim.SetBool("runRight", true);
            movement = (transform.right * Input.GetAxis("Horizontal"));
            movement = movement.normalized * moveSpeed;
            character.Move(movement * Time.deltaTime);
        }
        else
        {
            //anim.SetBool("runRight", false);
        }
    }

    public void KillPlayer()
    {
        Debug.Log("Player Killed");
    }
}

// 2. Sword push enemies
// 3. When character heavy attacks neeeds to stay in position on finish
// 4. Set //animation using trigger
// 6. Stop moving when attacking