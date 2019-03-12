﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteCameraController : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool useOffsetVals;
    [SerializeField] private float rotateSpeed;

    // Use this for initialization
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        if (!useOffsetVals)
        {
            offset = target.position - transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rotates player
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);
        float yAngle = target.eulerAngles.y;

        //Changes and rotates the player based on camera
        Quaternion camTurnAngle = Quaternion.Euler(0, yAngle, 0);
        transform.position = Vector3.Lerp(gameObject.transform.position, target.position - (camTurnAngle * offset),1f);
        transform.LookAt(target);
    }
}