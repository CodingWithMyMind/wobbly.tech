using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    float shootForce = 10;

    [SerializeField]
    Transform barrelTip;

    [SerializeField]
    private float forwardSpeed = 5;

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

        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed * forwardInput);
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject shot = GameObject.Instantiate(projectilePrefab, barrelTip.position, transform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(barrelTip.forward * shootForce);
        }
    }


}
