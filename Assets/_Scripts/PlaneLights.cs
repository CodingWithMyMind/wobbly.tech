using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneLights : MonoBehaviour
{
    [SerializeField]
    GameObject[] lights;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TurnLightsOn", 0, 2.5f);
        InvokeRepeating("TurnLightsOn", 0.4f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnLightsOn()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(true);
        }
        InvokeRepeating("TurnLightsOff", 0.2f, 1f);
        Random.Range()
        
    }
    public void TurnLightsOff()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
    }
}
