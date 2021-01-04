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
        // make new gameobject for the grid
        gridHolder = new GameObject("Grid").transform;

        gridHolder.SetParent(this.transform);
        gridHolder.localPosition=Vector3.zero;
        

        // starting cell position
        Vector3 cellPosition = new Vector3((rows/2),(columns/2),0);




        float cellsize = 1f;
        Vector3 scaleChange = new Vector3(cellsize,cellsize,cellsize);
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {

                cellPosition.x = x * cellsize + this.transform.position.x - (rows / 2) + (cellsize/2) ;
                cellPosition.y = y* cellsize + this.transform.position.y - (columns/2) + (cellsize/2);
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
        gridHolder.localScale = new Vector3(1f, 1f, 1f);
    }
    public void KillAll()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                cellMatrix[x, y].GetComponent<CellScript>().Kill();
            }
        }
    }

    public void AllAlive()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (Random.Range(0, 2) > 0)
                {
                    cellMatrix[x, y].GetComponent<CellScript>().Alive();
                }
            }
        }
    }
}
