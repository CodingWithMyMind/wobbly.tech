using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{

    [SerializeField]
    private float forwardSpeed = 5;

    [SerializeField]
    private float turnSpeed = 10;

    public AudioSource movementAudioSource; 
    // Audio clip of engine at idle
    public AudioClip engineIdlingSound;
    // Audio clip of engine driving
    public AudioClip engineDrivingSound;

    public Track LeftTrack;
    public Track RightTrack;



    public float pitchRange = 0.2f;

    public Transform centreOfMass;


    private float horizontalInput;
    private float forwardInput;

    private string movementAxisName = "Vertical";          // The name of the input axis for moving forward and back.
    private string turnAxisName = "Horizontal";              // The name of the input axis for turning.
    private Rigidbody rigidbody;              // Reference used to move the tank.
    private float movementInputValue;         // The current value of the movement input.
    private float turnInputValue;             // The current value of the turn input.
    private float originalPitch;



    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        originalPitch = movementAudioSource.pitch;
    }

    private void Update()
    {
        // Store the value of both input axes.
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);
        //rigidbody.centerOfMass = centreOfMass.position;
        EngineAudio();
    }


    private void EngineAudio()
    {
        // Check to see if tank is stationary / no input values
        if (Mathf.Abs(movementInputValue) < 0.1f && Mathf.Abs(turnInputValue) < 0.1f)
        {
            // if the audio player is currently playing tank driving sound...
            if (movementAudioSource.clip == engineDrivingSound)
            {
                // ... change the clip to idling and play it.
                movementAudioSource.clip = engineIdlingSound;
                movementAudioSource.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudioSource.Play();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (movementAudioSource.clip == engineIdlingSound)
            {
                // ... change the clip to driving and play.
                movementAudioSource.clip = engineDrivingSound;
                movementAudioSource.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudioSource.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        
        Drive();
        Turn();
    }


    private void Drive()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * movementInputValue * forwardSpeed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rigidbody.MovePosition(rigidbody.position + movement);

        //LeftTrack.Drive(movement, rigidbody);
        //RightTrack.Drive(movement, rigidbody);

        
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = turnInputValue * turnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    }
}





