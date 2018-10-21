using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Left,Right,Up,Down
}

public class InputManager : MonoBehaviour {

    private GameManager gameManager;
    public AudioClip moveSound;

    private void Awake()
    {
        gameManager = transform.GetComponent<GameManager>();
    }

	// Update is called once per frame
	void Update () {
        if(gameManager.State == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Right Move
                gameManager.Move(MoveDirection.Right);
                SoundManager.instance.PlayMusic(moveSound);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Left Move
                gameManager.Move(MoveDirection.Left);
                SoundManager.instance.PlayMusic(moveSound);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Up Move
                gameManager.Move(MoveDirection.Up);
                SoundManager.instance.PlayMusic(moveSound);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //Right Move
                gameManager.Move(MoveDirection.Down);
                SoundManager.instance.PlayMusic(moveSound);
            }
        }
    }
}
