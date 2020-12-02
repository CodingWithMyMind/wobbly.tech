using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimalSpawner : MonoBehaviour
{

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    GameObject[] objects;

    [SerializeField]
    int randomXRange;

    [SerializeField]
    bool randomXPos;

    //GameObject spawnedObjects;

    int index;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 3, 1);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void Spawn()
    {
        index = Random.Range(0, objects.Length);
        int xOffSet = 0; ;
        if (randomXPos)
        {
           xOffSet = Random.Range(-randomXRange, randomXRange + 1);
        }
        Instantiate(objects[index], new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z), objects[index].transform.rotation);
        
    }
}
