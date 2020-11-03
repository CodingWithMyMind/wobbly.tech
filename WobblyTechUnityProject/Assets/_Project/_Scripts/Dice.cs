
using System;
using System.Security.Cryptography;
using UnityEngine;



public class Dice : MonoBehaviour
{
    public Transform[] Numbers;

    private Rigidbody rb;

    bool rollComplete;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // check to see if rigid body stopped moving
        if(rb.velocity == Vector3.zero)
        {
            // check to see if the roll calculation is completed
            if (!rollComplete)
            {
                // really high starting number that should always get replaced
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

                // sum of opposite sides add to 7 so can figure top number from 7 - bnumber facing down
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
