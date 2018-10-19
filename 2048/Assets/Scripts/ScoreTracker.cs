using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

    private int score;
    public static ScoreTracker Instance;
    public Text scoreText;
    public Text highScoreText;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            scoreText.text = score.ToString();

            if (PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
                highScoreText.text = score.ToString();
            }
        }
    }

    private void Awake()
    {
        //Delete Player's preference
        //PlayerPrefs.DeleteAll();

        Instance = this;
        if (!PlayerPrefs.HasKey("HighScore")) //?
            PlayerPrefs.SetInt("HighScore", 0);
        scoreText.text = "0";
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
