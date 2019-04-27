
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteCameraController : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotateSpeed;
    Vector3 offsetRuntime;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        //calculateNewOffset();
        //rotates player
        //float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        //target.Rotate(0, horizontal, 0);
        float yAngle = target.eulerAngles.y;

        //Changes and rotates the player based on camera
        Quaternion camTurnAngle = Quaternion.Euler(0, yAngle, 0);

        float maxRange = 5;
        RaycastHit hit;

        Vector3 offsetRuntime;
        if (Physics.Raycast(transform.position, (target.position - transform.position), out hit, maxRange))
        {
            if (hit.transform.name != "aj")
            {
                // In Range and i can see you!
                print("Going blind");
                this.offsetRuntime = target.position - hit.point;
            }
            else
            {
                print("Aj is visible to me");
                RaycastHit hit2;
                //this.offsetRuntime = offset;
                if (Physics.Raycast(transform.position, (transform.position - target.position), out hit2, 1)){
                    if (!hit2.transform){
                        print("And there is no wall behind me");
                        this.offsetRuntime = offset;
                    }
                    else{
                        var calculatedOffset = new Vector3(offset.x, offset.y, offset.z - hit2.distance);
                        this.offsetRuntime = offset;
                        print("And i am sensing a wall behind me");
                    }
                }
                    
            }
        }

        transform.position = Vector3.Lerp(transform.position, target.position - (camTurnAngle * this.offsetRuntime), 2f);

        transform.LookAt(target);
        transform.Rotate(Vector3.left, 20);
    }

    //void calculateNewOffset(){
    ////Check physics hit object
    //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
    //RaycastHit hit;
    //if (Physics.Raycast(ray, out hit)){
    //    if (hit.transform.name != "aj"){
    //            //Check wall distance from Aj
    //        var distance = target.transform.localPosition - hit.transform.localPosition;
    //        oldOffset = offset;
    //            offset = distance;

    //    }else{

    //        print("I'm looking at " + hit.transform.name);
    //        offset = oldOffset;
    //    }
    //}
    //else{
    //    offset = oldOffset;
    //}
    //While the hitobject is not AJ{


    //IF it is not AJ
    //}
}