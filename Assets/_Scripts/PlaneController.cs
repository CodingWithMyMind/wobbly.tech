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
    private bool takenOff = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void TakeOff()
    {
        takenOff = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal2");
        forwardInput = Input.GetAxis("Vertical2");

        if (takenOff)
        {
            transform.Translate(transform.forward * Time.deltaTime * forwardSpeed, Space.World);

            transform.Rotate(Vector3.back * Time.deltaTime * turnSpeed * horizontalInput);

            transform.Rotate(Vector3.left * Time.deltaTime * pitchSpeed * -forwardInput);
        }

    }
}
