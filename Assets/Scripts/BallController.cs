using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class BallController : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        if (IsOwner)
        {
            Destroy(gameObject);
        }
    }
}
