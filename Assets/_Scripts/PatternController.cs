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

                int randomIconIndex = UnityEngine.Random.Range(0,icons.Length);
                GameObject instance = Instantiate(icons[randomIconIndex],canvas.transform);
                Debug.Log("spawning x:" + x + " y:" + y);

                Vector3 pos = new Vector3(x * 100, y * 100, 0);
                Debug.Log(pos);

                instance.transform.SetPositionAndRotation(pos, Quaternion.identity);

                spawnedIcons.Add(instance);
                //instance.transform.position = pos;
                //spawnedIcons[index] = instance;
                index++;
            }
        }
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
        for (int i = 0; i < spawnedIcons.Count; i++)
        {
            
            if(spawnedIcons[i].transform.position.y == row*100)
            {
                spawnedIcons[i].transform.DOPunchScale(Vector3.one , 0.5f, 10, 10);
            }
        }
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
