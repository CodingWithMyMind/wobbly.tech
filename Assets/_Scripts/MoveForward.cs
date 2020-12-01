using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5;

    float topBounds = 20;
    float bottomBounds = -20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > topBounds || transform.position.z < bottomBounds)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }
}
