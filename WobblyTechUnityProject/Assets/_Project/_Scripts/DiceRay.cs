
using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class DiceRay : MonoBehaviour
{
    // array of the transforms of the sides. The actual number value of the side is array index + 1
    public Transform[] Sides;
    // reference to the rigidbody
    private Rigidbody rb;

    bool rollComplete;

    public GameObject TextFeedback;
    private RaycastHit objectHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


        float rand = UnityEngine.Random.Range(-10.0f, 10f);
        Vector3 randomRotation = new Vector3(rand, rand, rand);
        rb.AddTorque(randomRotation* 3000, ForceMode.Impulse);
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
                int numberFacingDown = 0;
                float shortestRay = 1000f;

               // for loop to iterate all the side transforms
                for (int i = 0; i < Sides.Length; i++)
                {
                    RaycastHit hit;
                    Vector3 fwd = Sides[i].forward;
                    Vector3 pos = Sides[i].position;

                    if (Physics.Raycast(pos, fwd, out hit, 50))
                    {
                        Debug.DrawRay(pos, fwd, Color.blue, 100.0f,false);
                        if (hit.distance < shortestRay)
                        {
                            shortestRay = hit.distance;
                            numberFacingDown = i + 1;
                            Debug.Log("shortest ray is " + hit.distance + "by side " + numberFacingDown);

                        }
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
