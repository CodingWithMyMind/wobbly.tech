using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    Camera[] allCameras;

    // Start is called before the first frame update
    void Start()
    {
        allCameras = FindObjectsOfType<Camera>();
        Debug.Log(allCameras.Length + "Cameras Found");


    }


    public void ActivateCamera(Camera cameraToTurnOn)
    {
        foreach(Camera c in allCameras)
        {
            c.enabled = false;
        }
        cameraToTurnOn.enabled = true;
    }
}
