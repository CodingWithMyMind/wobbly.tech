using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PatternController : MonoBehaviour
{
    public GameObject[] icons;

    public Camera camera;

    public GameObject canvas;

    public int rows = 3;
    public int columns = 3;

    Vector3 lastInRowLocation;
    Vector3 firstInRowLocation;


    // floats to store the high/lowest x positions
    float highestX = 0;
    float lowestX = 10000;


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

                index++;
            }
        }
    }

    private void SpawnIcon(Vector3 spawnPosition)
    {
        // choose random icon and spawn it
        int randomIconIndex = UnityEngine.Random.Range(0, icons.Length);
        GameObject instance = Instantiate(icons[randomIconIndex], canvas.transform);
        // set position of new spawned item to passed in position
        instance.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        // add to spawned items list
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
        // choose a random row
        int row = UnityEngine.Random.Range(0, columns);

        // random choice of row moving left or right
        bool shouldMoveLeft = (UnityEngine.Random.Range(0f, 1f) > 0.5f);



        for (int i = 0; i < spawnedIcons.Count; i++)
        {
            // if the spawned icon has is on the correct y value/row
            if(spawnedIcons[i].transform.position.y == row*100)
            {

                // move the row
                MoveRow(i,shouldMoveLeft);

                AddAndReplaceIcons(i);

                //

            }
        }

        if (!shouldMoveLeft)
        {
            SpawnIcon(lastInRowLocation);
            //Destroy
        }
        if (shouldMoveLeft)
        {
            SpawnIcon(firstInRowLocation);
        }

    }

    private void AddAndReplaceIcons(int i)
    {
        // if current icon has higher x than the previous highest x
        if (spawnedIcons[i].transform.position.x > highestX)
        {
            // the location of the last icon in the row
            lastInRowLocation = spawnedIcons[i].transform.position;
            // set new highest x
            highestX = spawnedIcons[i].transform.position.x;
        }
        if (spawnedIcons[i].transform.position.x < lowestX)
        {
            // the location of the first icon in the row
            lowestX = spawnedIcons[i].transform.position.x;
            // set new lowest x
            firstInRowLocation = spawnedIcons[i].transform.position;
        }
    }

    private void MoveRow(int index,bool moveLeft)
    {
        Vector3 endMoveLocation;
        if (moveLeft)
        {
            // end location of the current icon should be its current possiton but + 100 on x
            endMoveLocation = new Vector3(spawnedIcons[index].transform.position.x + 100, spawnedIcons[index].transform.position.y, spawnedIcons[index].transform.position.z);
        }
        else
        {
            // end location of the current icon should be its current possiton but + 100 on x
            endMoveLocation = new Vector3(spawnedIcons[index].transform.position.x - 100, spawnedIcons[index].transform.position.y, spawnedIcons[index].transform.position.z);
        }
        // apply movement
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
