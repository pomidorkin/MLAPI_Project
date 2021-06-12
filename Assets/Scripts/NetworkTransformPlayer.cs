using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MLAPI;

public class NetworkTransformPlayer : NetworkBehaviour
{

    private float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // We need to update the position of the object oly if we are the owner of that object
    void Update()
    {
        if (IsClient && IsOwner)
        {
            Move();
        }
    }

    private void Move()
    {
        // GetKey - continous checking, GetKeyDown - checking if the key was pressed once
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }

    }
}
