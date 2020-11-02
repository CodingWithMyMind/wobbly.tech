
using System;
using System.Security.Cryptography;
using UnityEngine;



public class Dice : MonoBehaviour
{
    public Transform one;
    public Transform two;
    public Transform three;
    public Transform four;
    public Transform five;
    public Transform six;

    public Transform[] Numbers;

    private Rigidbody rb;

    public bool stationary;

    bool rollComplete;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity == Vector3.zero)
        {
            if (!rollComplete)
            {
                float lowestYThisRoll = 1000;
                int numberFacingDown = 0;


                for (int i = 0; i < Numbers.Length; i++)
                {
                    if (Numbers[i].position.y < lowestYThisRoll)
                    {
                        lowestYThisRoll = Numbers[i].position.y;
                        numberFacingDown = i + 1;
                    }
                }

                int numberFacingUp = 7 - numberFacingDown; 
                Debug.Log(numberFacingUp);
                rollComplete = true;
            }
        }
        else
        {
            rollComplete = false;
        }
    }
}
