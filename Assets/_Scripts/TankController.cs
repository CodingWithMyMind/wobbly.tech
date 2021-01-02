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
    public AudioClip engineIdilingSound;
    // Audio clip of engine driving
    public AudioClip engineDrivingSound;
    
    public float m_PitchRange = 0.2f;


    private float horizontalInput;
    private float forwardInput;

    private string movementAxisName = "Vertical";          // The name of the input axis for moving forward and back.
    private string turnAxisName = "Horizontal";              // The name of the input axis for turning.
    private Rigidbody rigidbody;              // Reference used to move the tank.
    private float movementInputValue;         // The current value of the movement input.
    private float turnInputValue;             // The current value of the turn input.
    private float originalPitch;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        // Store the value of both input axes.
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);

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
                movementAudioSource.clip = engineIdling;
                movementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                movementAudio.Play();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (movementAudioSource.clip == engineIdlingSound)
            {
                // ... change the clip to driving and play.
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed * forwardInput);
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

    }


}
