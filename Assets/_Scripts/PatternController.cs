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


    // floats to store the high/lowest x positions
    float lowestX = 10000;
    float highestX = 0;
    // highest lowest gameobjects
    GameObject highestXObj;
    GameObject lowestXObj;

    // floats to store the high/lowest y positions
    float lowestY = 10000;
    float highestY = 0;
    // highest lowest gameobjects
    GameObject highestYObj;
    GameObject lowestYObj;

    private List<GameObject> spawnedIcons = new List<GameObject>();

    //public GameObject[] spawnedIcons;
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        // initial spawn icons loop
        for (int y = 0; y < columns; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                Vector3 pos = new Vector3(x * 100, y * 100, 0);
                SpawnIcon(pos);
                index++;
            }
        }

        // make camera be at middle of the icons
        Vector3 newCameraPos = new Vector3((44.444f * rows), (44.444f * columns), camera.transform.position.z);


        camera.orthographicSize = rows * 40;

        camera.transform.position = newCameraPos;

        InvokeRepeating("CheckIfInRow", 0, 4);
        InvokeRepeating("CheckIfInColumn", 2, 4);
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
        int row = UnityEngine.Random.Range(1, columns-1);
        // random choice of row moving left or right
        bool shouldMoveLeft = (UnityEngine.Random.Range(0f, 1f) > 0.5f);

        // loop through all icons
        for (int i = 0; i < spawnedIcons.Count; i++)
        {
            // if the spawned icon has is on the correct y value/row
            if (spawnedIcons[i].transform.position.y == row * 100)
            {
                MoveRow(i, shouldMoveLeft);
                HighestLowestX(i);
            }
        }

        AddAndReplaceIconsX(shouldMoveLeft);

        // reset values
        highestX = 0;
        lowestX = 100000;
    }

    private void AddAndReplaceIconsX(bool shouldMoveLeft)
    {
        if (!shouldMoveLeft)
        {
            // spawn a new icon at the highest x location
            SpawnIcon(highestXObj.transform.position);
            // remove the lowest x object from the lit and destroy
            spawnedIcons.Remove(lowestXObj);
            Destroy(lowestXObj);
        }
        if (shouldMoveLeft)
        {
            // spawn a new icon at the lowest x location
            SpawnIcon(lowestXObj.transform.position);
            // remove the highest x object from the list and destroy
            spawnedIcons.Remove(highestXObj);
            Destroy(highestXObj);
        }
    }

    private void AddAndReplaceIconsY(bool shouldMoveUp)
    {
        if (!shouldMoveUp)
        {
            // spawn a new icon at the highest y location
            SpawnIcon(highestYObj.transform.position);
            // remove the lowest y object from the list and destroy
            spawnedIcons.Remove(lowestYObj);
            Destroy(lowestYObj);
        }
        if (shouldMoveUp)
        {
            // spawn a new icon at the lowest y location
            SpawnIcon(lowestYObj.transform.position);
            // remove the highest y object from the list and destroy
            spawnedIcons.Remove(highestYObj);
            Destroy(highestYObj);
        }
    }


    private void HighestLowestX(int i)
    {
        // if current icon has higher x than the previous highest x
        if (spawnedIcons[i].transform.position.x > highestX)
        {
            highestX = spawnedIcons[i].transform.position.x;
            highestXObj = spawnedIcons[i];
        }
        if (spawnedIcons[i].transform.position.x < lowestX)
        {
            lowestX = spawnedIcons[i].transform.position.x;
            lowestXObj = spawnedIcons[i];
        }
    }

    private void MoveRow(int index, bool moveLeft)
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
        spawnedIcons[index].transform.DOMove(endMoveLocation, 2).SetEase(Ease.InOutQuart);
    }

    private void CheckIfInColumn()
    {
        // choose a random column
        int column = UnityEngine.Random.Range(1, columns-1);
        // random choice of row moving left or right
        bool shouldMoveUp = (UnityEngine.Random.Range(0f, 1f) > 0.5f);

        // loop through all icons
        for (int i = 0; i < spawnedIcons.Count; i++)
        {
            // if the spawned icon has is on the correct y value/row
            if (spawnedIcons[i].transform.position.x ==  column * 100)
            {
                MoveColumn(i, shouldMoveUp);
                HighestLowestY(i);
            }
        }

        AddAndReplaceIconsY(shouldMoveUp);

        // reset values
        highestY = 0;
        lowestY = 100000;
    }

    private void MoveColumn(int index, bool moveUp)
    {
        Vector3 endMoveLocation;
        if (moveUp)
        {
            // end location of the current icon should be its current possiton but + 100 on x
            endMoveLocation = new Vector3(spawnedIcons[index].transform.position.x, spawnedIcons[index].transform.position.y + 100, spawnedIcons[index].transform.position.z);
        }
        else
        {
            // end location of the current icon should be its current possiton but + 100 on x
            endMoveLocation = new Vector3(spawnedIcons[index].transform.position.x , spawnedIcons[index].transform.position.y - 100, spawnedIcons[index].transform.position.z);
        }
        // apply movement
        spawnedIcons[index].transform.DOMove(endMoveLocation, 2).SetEase(Ease.InOutQuart);
    }

    private void HighestLowestY(int i)
    {
        // if current icon has higher x than the previous highest x
        if (spawnedIcons[i].transform.position.y > highestY)
        {
            highestY = spawnedIcons[i].transform.position.y;
            highestYObj = spawnedIcons[i];
        }
        if (spawnedIcons[i].transform.position.y < lowestY)
        {
            lowestY = spawnedIcons[i].transform.position.y;
            lowestYObj = spawnedIcons[i];
        }
    }
}
