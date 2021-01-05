using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{

    bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        Debug.Log(grounded);
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    public void Drive(Vector3 direction, Rigidbody rb)
    {
        //if (grounded)
        {
            rb.MovePosition(transform.position + direction);
        }
    }
}
