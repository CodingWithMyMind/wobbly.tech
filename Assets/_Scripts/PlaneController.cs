using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{



    [SerializeField]
    private float forwardSpeed = 5;

    [SerializeField]
    private float pitchSpeed = 10;

    [SerializeField]
    private float turnSpeed = 10;

    private float horizontalInput;
    private float forwardInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        
        transform.Translate(transform.forward * Time.deltaTime * forwardSpeed, Space.World);

        transform.Rotate(Vector3.back * Time.deltaTime * turnSpeed * horizontalInput);

        transform.Rotate(Vector3.left * Time.deltaTime * pitchSpeed * -forwardInput);

    }
}
