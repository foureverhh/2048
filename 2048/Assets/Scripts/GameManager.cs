using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    private Tile[,] alltiles = new Tile[4, 4];
    private List<Tile> emptyTiles = new List<Tile>();
    private List<Tile[]> columns = new List<Tile[]>();
    private List<Tile[]> rows = new List<Tile[]>();


	// Use this for initialization
	void Start () {
        Tile[] allTilesOneDim = transform.GetComponentsInChildren<Tile>();
       
        //Tile[] allTilesOneDim = GameObject.FindObjectsOfType<Tile>();
        //Debug.Log(allTilesOneDim.Length);
        foreach (Tile t in allTilesOneDim)
        {
            t.Number = 0;
            alltiles[t.indexRow,t.indexCol] = t;
            emptyTiles.Add(t);
        }

        columns.Add(new Tile[] { alltiles[0, 0], alltiles[1, 0], alltiles[2, 0], alltiles[3, 0] });
        columns.Add(new Tile[] { alltiles[0, 1], alltiles[1, 1], alltiles[2, 1], alltiles[3, 1] });
        columns.Add(new Tile[] { alltiles[0, 2], alltiles[1, 2], alltiles[2, 2], alltiles[3, 2] });
        columns.Add(new Tile[] { alltiles[0, 3], alltiles[1, 3], alltiles[2, 3], alltiles[3, 3] });

        rows.Add(new Tile[] { alltiles[0, 0], alltiles[0, 1], alltiles[0, 2], alltiles[0, 3] });
        rows.Add(new Tile[] { alltiles[1, 0], alltiles[1, 1], alltiles[1, 2], alltiles[1, 3] });
        rows.Add(new Tile[] { alltiles[2, 0], alltiles[2, 1], alltiles[2, 2], alltiles[2, 3] });
        rows.Add(new Tile[] { alltiles[3, 0], alltiles[3, 1], alltiles[3, 2], alltiles[3, 3] });

        //Creat two tiles
        Generate();
        Generate();
    }

    bool MakeOneMoveDownIndex(Tile[] LineOfTiles)
    {
        for (int i = 0; i < LineOfTiles.Length - 1; i++)
        {
            //MOVE BLOCK
            //En empty tile besides a tile
            if (LineOfTiles[i].Number == 0 && LineOfTiles[i + 1].Number != 0)
            {
                LineOfTiles[i].Number = LineOfTiles[i + 1].Number;
                LineOfTiles[i + 1].Number = 0;
                return true;
            }
            //Merge block
            if (LineOfTiles[i].Number !=0 &&
                LineOfTiles[i].Number == LineOfTiles[i + 1].Number && 
                LineOfTiles[i].mergedThisTurn == false && 
                LineOfTiles[i + 1].mergedThisTurn == false)
            {
                LineOfTiles[i].Number = LineOfTiles[i].Number * 2;
                LineOfTiles[i + 1].Number = 0;
                LineOfTiles[i].mergedThisTurn = true;
                //LineOfTiles[i + 1].mergedThisTurn = true;
                return true;
            }
        }
        return false;
    }

    bool MakeOneMoveUpIndex(Tile[] LineOfTiles)
    {
        for (int i = LineOfTiles.Length - 1; i > 0 ; i--)
        {
            //MOVE BLOCK
            //En empty tile besides a tile
            if (LineOfTiles[i].Number == 0 && LineOfTiles[i - 1].Number != 0)
            {
                LineOfTiles[i].Number = LineOfTiles[i - 1].Number;
                LineOfTiles[i - 1].Number = 0;
                return true;
            }

            //Merge block
            if (LineOfTiles[i].Number != 0 &&
               LineOfTiles[i].Number == LineOfTiles[i - 1].Number &&
               LineOfTiles[i].mergedThisTurn == false &&
               LineOfTiles[i - 1].mergedThisTurn == false)
            {
                LineOfTiles[i].Number = LineOfTiles[i].Number * 2;
                LineOfTiles[i -1].Number = 0;
                LineOfTiles[i].mergedThisTurn = true;
                //LineOfTiles[i-1].mergedThisTurn = true;
                return true;
            }
        }
        return false;
    }

    //Random generate new tiles
    void Generate()
    {
        if(emptyTiles.Count > 0)
        {
            int indexForNewNunmber = Random.Range(0,emptyTiles.Count);
            int randomNum = Random.Range(0,10);
            if (randomNum == 0)
                emptyTiles[indexForNewNunmber].Number = 4;
            else
                emptyTiles[indexForNewNunmber].Number = 2;
            emptyTiles.RemoveAt(indexForNewNunmber);
            //Debug.Log("It runs here in Generate");
        }
    }
    // Update is called once per frame
    //void Update () {
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        Generate();
    //    }

    //}

    void ResetMergedFlags()
    {
        foreach(Tile t in alltiles)
        {
            t.mergedThisTurn = false;
        }
    }

    private void UpdateEmptyTiles()
    {
        emptyTiles.Clear();
        foreach(Tile t in alltiles)
        {
            if (t.Number == 0)
                emptyTiles.Add(t);
        }
    }
    public void Move(MoveDirection md)
    {
        Debug.Log(md.ToString() + " move.");
        bool moveMade = false;
        ResetMergedFlags();
        for (int i=0; i < rows.Count; i++)
        {
            switch(md)
            {
                case MoveDirection.Down:
                    while (MakeOneMoveUpIndex(columns[i]))
                    {
                        moveMade = true;
                    }
                    break;
                case MoveDirection.Up:
                    while (MakeOneMoveDownIndex(columns[i]))
                    {
                        moveMade = true;
                    }
                    break;
                case MoveDirection.Left:
                    while (MakeOneMoveDownIndex(rows[i]))
                    {
                        moveMade = true;
                    }
                    break;
                case MoveDirection.Right:
                    while (MakeOneMoveUpIndex(rows[i]))
                    {
                        moveMade = true;
                    }
                    break;
            }
        }
        if (moveMade)
        {
            //To check whether there is en empty blcok
            UpdateEmptyTiles();
            //Create one tile after each move
            Generate();
        }
    }
}
