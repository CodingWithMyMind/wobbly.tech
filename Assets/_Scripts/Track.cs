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

// todo on trigger enter working for track state

    private void OnTriggerExit(Collider other)
    {
        grounded = false;
    }

}
