using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using System;

public class BallNetworkTransform : NetworkBehaviour
{
    private float riseSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            Rise();
        }
    }

    private void Rise()
    {
        transform.Translate(Vector3.up * Time.deltaTime * riseSpeed);
    }
}
