using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour {

    private GameManager gameManager;

    public float maxTouchTime;
    public float minTouchDistance;

    private float touchStartTime;
    private float touchEndTime;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    // Use this for initialization
    void Awake () {
        gameManager = GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.State == GameState.Playing)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began :
                            touchStartPos = touch.position;
                            touchStartTime = Time.time;
                            break;

                        case TouchPhase.Ended :
                            touchEndPos = touch.position;
                            touchEndTime = Time.time;
                            Vector2 distance = touchEndPos - touchStartPos;
                            if (distance.magnitude > minTouchDistance && (touchEndTime - touchStartTime) < maxTouchTime)
                            {
                                if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y) && distance.x > 0)
                                {
                                    gameManager.Move(MoveDirection.Right);
                                    Debug.Log("Right");
                                }
               
                                if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y) && distance.x < 0)
                                {
                                    gameManager.Move(MoveDirection.Left); Debug.Log("Left");
                                }

                                if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y) && distance.y > 0)
                                {
                                    gameManager.Move(MoveDirection.Up); Debug.Log("up");
                                }

                                if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y) && distance.y < 0)
                                {
                                    gameManager.Move(MoveDirection.Down); Debug.Log("down");
                                }
                            }
                            break;
                    }
            }
        }
    }
}

