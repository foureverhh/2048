using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Left,Right,Up,Down
}

public class InputManager : MonoBehaviour {

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = transform.GetComponent<GameManager>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(gameManager.State == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Right Move
                gameManager.Move(MoveDirection.Right);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Left Move
                gameManager.Move(MoveDirection.Left);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Up Move
                gameManager.Move(MoveDirection.Up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //Right Move
                gameManager.Move(MoveDirection.Down);
            }
        }
    }
}
