using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public static PlayerManager instance;

    public Rigidbody rb;

    public float speed;
    [SerializeField] public TextMeshProUGUI life;

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
        life.text = "x " + PlayerController.lives;
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

    public void KillAnimation()
    {
        animator.SetBool("isDead", true);
        //StartCoroutine(OnCompletedDeathAnimation());
        //Call menu method

        Destroy(gameObject);
        PlayerController.lives--;
        life.text = "x " + PlayerController.lives;
        print("Lives " + PlayerController.lives);
        if (PlayerController.lives >= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
            SceneManager.LoadScene("Menu");
        //GameManager.CurrentGameState = GameState.MainMenu;
    }

    IEnumerator OnCompletedDeathAnimation()
    {
        AnimatorClipInfo[] a = animator.GetCurrentAnimatorClipInfo(0);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < a[0].clip.length)
            yield return new WaitForSeconds(a[0].clip.length - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        // TODO: Do something when animation did complete
        //Destroy(gameObject);
        //PlayerController.lives--;
        //print("Lives " + PlayerController.lives);
        //if (PlayerController.lives >= 0)
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //else
        //    SceneManager.LoadScene("Menu");
        //    //GameManager.CurrentGameState = GameState.MainMenu;

    }
}
