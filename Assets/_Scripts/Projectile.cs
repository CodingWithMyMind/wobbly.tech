using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
   float bulletForce = 10;


    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        /*//rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * bulletForce , ForceMode.Impulse);*/

        //b.AddForce(bulletForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
