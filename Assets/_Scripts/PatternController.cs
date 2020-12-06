using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PatternController : MonoBehaviour
{
    public GameObject[] icons;

    public GameObject canvas;

    public int rows = 3;
    public int columns = 3;

    Vector3 lastInRowLocation;
    Vector3 firstInRowLocation;


    public List<GameObject> spawnedIcons = new List<GameObject>();

    //public GameObject[] spawnedIcons;
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        for (int y = 0; y < columns; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                

                
                


                Vector3 pos = new Vector3(x * 100, y * 100, 0);
                Debug.Log(pos);

                SpawnIcon(pos);

                
                //instance.transform.position = pos;
                //spawnedIcons[index] = instance;
                index++;
            }
        }
    }

    private void SpawnIcon(Vector3 spawnPosition)
    {
        int randomIconIndex = UnityEngine.Random.Range(0, icons.Length);
        GameObject instance = Instantiate(icons[randomIconIndex], canvas.transform);
        Debug.Log("spawning at:" + spawnPosition);
        instance.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);

        spawnedIcons.Add(instance);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CheckIfInRow();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CheckIfInColumn();
        }

    }

    private void CheckIfInRow()
    {

        int row = UnityEngine.Random.Range(0, columns);

        bool shouldMoveLeft = (UnityEngine.Random.Range(0f, 1f) > 0.5f);

        float highestX = 0;
        float lowestX = 10000;




        for (int i = 0; i < spawnedIcons.Count; i++)
        {
            if(spawnedIcons[i].transform.position.y == row*100)
            {
                MoveRow(i,shouldMoveLeft);

                if(spawnedIcons[i].transform.position.x > highestX)
                {
                    highestX = spawnedIcons[i].transform.position.x;
                    lastInRowLocation = spawnedIcons[i].transform.position;

                }
                if (spawnedIcons[i].transform.position.x < lowestX)
                {
                    lowestX = spawnedIcons[i].transform.position.x;
                    firstInRowLocation = spawnedIcons[i].transform.position;
                }
            }

        }

        if (!shouldMoveLeft)
        {
            SpawnIcon(lastInRowLocation);
        }
        if (shouldMoveLeft)
        {
            SpawnIcon(firstInRowLocation);
        }

    }

    private void MoveRow(int index,bool moveLeft)
    {
        Vector3 endMoveLocation;
        if (moveLeft)
        {
            endMoveLocation = new Vector3(spawnedIcons[index].transform.position.x + 100, spawnedIcons[index].transform.position.y, spawnedIcons[index].transform.position.z);
        }
        else
        {
            endMoveLocation = new Vector3(spawnedIcons[index].transform.position.x - 100, spawnedIcons[index].transform.position.y, spawnedIcons[index].transform.position.z);
        }
        spawnedIcons[index].transform.DOMove(endMoveLocation, 1);

    }

    private void CheckIfInColumn()
    {
        int column = UnityEngine.Random.Range(0, rows);
        for (int i = 0; i < spawnedIcons.Count; i++)
        {
            if (spawnedIcons[i].transform.position.x == column * 100)
            {
                spawnedIcons[i].transform.DOPunchScale(Vector3.one , 0.5f, 10, 10);
            }
        }
    }
}
