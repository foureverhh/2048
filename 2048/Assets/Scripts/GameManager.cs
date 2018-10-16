using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private Tile[,] alltiles = new Tile[4, 4];

	// Use this for initialization
	void Start () {
        Tile[] allTilesOneDim = transform.GetComponentsInChildren<Tile>();
       
        //Tile[] allTilesOneDim = GameObject.FindObjectsOfType<Tile>();
        Debug.Log(allTilesOneDim.Length);
        foreach (Tile t in allTilesOneDim)
        {
            t.Number = 0;
            alltiles[t.indexRow,t.indexCol] = t;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Move(MoveDirection md)
    {
        Debug.Log(md.ToString() + " move.");
    }
}
