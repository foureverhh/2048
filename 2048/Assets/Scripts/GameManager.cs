using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    private Tile[,] alltiles = new Tile[4, 4];
    private List<Tile> emptyTiles = new List<Tile>();


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
    void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Generate();
        }
	}

    public void Move(MoveDirection md)
    {
        Debug.Log(md.ToString() + " move.");
    }
}
