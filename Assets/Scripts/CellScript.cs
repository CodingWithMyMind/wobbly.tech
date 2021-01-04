using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellScript : MonoBehaviour
{

    public bool isAlive;
    public Sprite aliveSprite;
    public Sprite deadSprite;

    public BoardManager boardScript;
    private GameObject[,] cellMatrix;
    private SpriteRenderer spriteRenderer;

    private int aliveNeighbours;
    private int x;
    private int y;
    private bool isSpriteSet;

    void Awake()
    {
        boardScript = BoardManager.instance;
        cellMatrix = boardScript.cellMatrix;
        spriteRenderer = GetComponent<SpriteRenderer>();


    }
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        

        if (Input.GetMouseButton(0))
        {
            isAlive = true;
            spriteRenderer.sprite = aliveSprite;
        }
        else if(Input.GetMouseButton(1))
        {
            isAlive = false;
            spriteRenderer.sprite = deadSprite;
        }

    }

    void Start()
    {
     
    }

    void Update()
    {

            if (isAlive)
            {
                spriteRenderer.sprite = aliveSprite;
            }
            else
            {
                spriteRenderer.sprite = deadSprite;
            }
    }

    public void ScanNeighbours()
    {
        aliveNeighbours = 0;

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i == x && j == y) continue;
                if (i >= 0 && i < boardScript.columns && j >= 0 && j < boardScript.rows)
                {
                    if (cellMatrix[i, j].GetComponent<CellScript>().isAlive)
                        aliveNeighbours++;
                }
            }
        }
    }

    public void Determine()
    {
 
        // cell carry on living if 2 or 3 neighbours
        if (isAlive && (aliveNeighbours == 2 || aliveNeighbours == 3))
        {
            isAlive = true;
        }
        // if cell not alive but has 3 neighbours then cell will come alive
        else if (!isAlive && aliveNeighbours == 3)
        {
            isAlive = true;
        }
        // otherwise die of over/under population
        else
        {
            isAlive = false;
        }
    }

    public void Kill()
    {
        isAlive = false;
    }

    public void Alive()
    {
        isAlive = true;
    }

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
