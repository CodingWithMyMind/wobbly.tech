using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimalSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject[] animals;

    [SerializeField]
    int xRange;

    int index;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAnimal", 3, 1);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void SpawnAnimal()
    {
        index = Random.Range(0, animals.Length);


        int x = Random.Range(-xRange, xRange + 1);

        Instantiate(animals[index], new Vector3(x, 1, 20), animals[index].transform.rotation);
    }
}
