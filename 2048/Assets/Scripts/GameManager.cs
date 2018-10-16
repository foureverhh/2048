using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Tile[] allTilesOneDim = transform.GetComponentsInChildren<Tile>();
       
        //Tile[] allTilesOneDim = GameObject.FindObjectsOfType<Tile>();
        Debug.Log(allTilesOneDim.Length);
        foreach (Tile t in allTilesOneDim)
        {
            t.Number = 0;
            Debug.Log("Tile is: " + t.gameObject.name);
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
