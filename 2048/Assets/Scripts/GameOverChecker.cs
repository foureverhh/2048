using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverChecker : MonoBehaviour {

    [HideInInspector]
    public GameManager gameManager;
    public Text gameOverScoreText;
    [SerializeField]
    private CanvasGroup gameOverCanvas;

    // Use this for initialization
    void Start () {
        //transform.SetActive = false;
        //GetComponent<Renderer>().enabled = false;
      // gameOverCanvas.alpha= 0f;
    }
	
	// Update is called once per frame
	void Update () {
        //if (!gameManager.TileCanMove())
        //{
        //    gameOverCanvas.alpha = 1f;
        //    gameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
        //}
    }
}
