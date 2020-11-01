using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{

    public GameObject cellPrefab;
    public int rows;
    public int columns;
    public float spawnChance;
    public GameObject[,] cellMatrix;

    Transform gridHolder;

    public static BoardManager instance = null;

    public float nextTime;

    private bool paused = false;

    void Start()
    {
        SetupScene();
        
    }

    void Update()
    {
        if (Time.time > nextTime && !paused)
        {
            
                nextTime = Time.time + 0.1f;
                NextGeneration();
            
        }
    }
    public void setPause()
    {
        paused = !paused;
    }

    void Awake()
    {
        cellMatrix = new GameObject[columns, rows];
        

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    }




    void NextGeneration()
    {
        // check every cell on x axis
        for (int x = 0; x < columns; x++)
        {
            // check every cell on y axis
            for (int y = 0; y < rows; y++)
            {
                cellMatrix[x, y].GetComponent<CellScript>().ScanNeighbours();
            }
        }
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                cellMatrix[x, y].GetComponent<CellScript>().Determine();
            }
            

        }

    }
    public void SetupScene()
    {
        gridHolder = new GameObject("Grid").transform;
        gridHolder.SetParent(this.transform);
        Vector3 cellPosition = Vector3.zero;
        float cellsize = 0.1f;
        Vector3 scaleChange = new Vector3(cellsize,cellsize,cellsize);
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                
                cellPosition.x = x* cellsize + this.transform.position.x;
                cellPosition.y = y* cellsize + this.transform.position.y;
                cellPosition.z = this.transform.position.z;

                GameObject cellInstance = Instantiate(cellPrefab, cellPosition, Quaternion.identity) as GameObject;
                cellInstance.transform.SetParent(gridHolder);
                cellInstance.transform.localScale = scaleChange;


                cellInstance.name = "Cell: " + x.ToString() + ","+ y.ToString();
                cellInstance.GetComponent<CellScript>().SetPosition(x, y);
                // set a reference to this cell in the cell matrix
                cellMatrix[x, y] = cellInstance;

                //Set the cell alive according with the chance of "spawnChance"
                cellInstance.GetComponent<CellScript>().isAlive = (Random.value <= spawnChance ? true : false);
            }
        }
    }
}
