
using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    // array of the transforms of the sides. The actual number value of the side is array index + 1
    public Transform[] Sides;
    // reference to the rigidbody
    private Rigidbody rb;

    bool rollComplete;

    public GameObject TextFeedback;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float rand = UnityEngine.Random.Range(-10.0f, 10f);
        Vector3 randomRotation = new Vector3(rand, rand, rand);
        rb.AddTorque(randomRotation* 300, ForceMode.Impulse);
    }

    void SpawnTextFeedback(int rollValue)
    {
        Vector3 spawnLoacation = new Vector3(transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        GameObject TextFeedbackInstance = Instantiate(TextFeedback, spawnLoacation, Quaternion.identity, this.transform);
        // make text look at camera
        TextFeedbackInstance.GetComponentInChildren<Text>().text = rollValue.ToString();
        
        TextFeedbackInstance.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    // Update is called once per framed
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

               // for loop to iterate all the side transforms
                for (int i = 0; i < Sides.Length; i++)
                {
                    // check to see if the current side in the array has the lowest y value/ closest to the ground
                    if (Sides[i].position.y < lowestYThisRoll)
                    {
                        lowestYThisRoll = Sides[i].position.y;

                        // the number facing down is 
                        numberFacingDown = i + 1;
                    }
                }

                // sum of opposite sides add to 7 so can figure top number from 7 - bnumber facing down
                int numberFacingUp = 7 - numberFacingDown;
                SpawnTextFeedback(numberFacingUp);
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
