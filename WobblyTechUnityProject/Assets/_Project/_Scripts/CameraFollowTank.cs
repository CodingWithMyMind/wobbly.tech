using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTank : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Tank;

    private Vector3 offset = new Vector3(0, 5, -7);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Tank.transform.position + offset;
    }
}
