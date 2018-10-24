using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    GameOver,
    WaitingForMoveToEnd
}

public class GameManager : MonoBehaviour {

    //New Ater Added Delays
    public GameState State;
    [Range(0, 2f)]
    public float delay;
    private bool moveMade;
    private bool[] lineMoveComplete = new bool[4] { true, true, true, true };

    private Tile[,] alltiles = new Tile[4, 4];
    private List<Tile> emptyTiles = new List<Tile>();
    private List<Tile[]> columns = new List<Tile[]>();
    private List<Tile[]> rows = new List<Tile[]>();
    private GameOverChecker gameOverChecker;

    public AudioSource winSound;
    public AudioSource lostsound;
    public AudioClip moveSound;
    public AudioClip lostMusic;
    public AudioClip winMusic;
   
	// Use this for initialization
	void Start ()
    {
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

        //Make connection to GameOverChecker
        gameOverChecker = transform.GetChild(1).GetComponent<GameOverChecker>();
        gameOverChecker.transform.gameObject.SetActive(false);
        //gameOverChecker.gameManager = this;
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
                LineOfTiles[i].PlayerMergedAnimation();
                LineOfTiles[i].mergedThisTurn = true;
                //LineOfTiles[i + 1].mergedThisTurn = true;
                ScoreTracker.Instance.Score += LineOfTiles[i].Number;
                CheckWin();
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
                LineOfTiles[i].PlayerMergedAnimation();
                LineOfTiles[i].mergedThisTurn = true;
                //LineOfTiles[i-1].mergedThisTurn = true;
                ScoreTracker.Instance.Score += LineOfTiles[i].Number;
                CheckWin();
            }
        }
        return false;
    }

    void CheckWin()
    {
        if (PlayerWin())
        {
            ShowWinResult();
        }
    }

    bool PlayerWin()
    {
        foreach (Tile t in alltiles)
        {
            if (t.Number == 2048)
                return true;
        }
        return false;
    }

    void ShowWinResult()
    {
        //Disable input
        GetComponent<InputManager>().enabled = false;
        GetComponent<TouchInputManager>().enabled = false;
        gameOverChecker.transform.gameObject.SetActive(true);
        gameOverChecker.transform.GetChild(0).gameObject.SetActive(false);
        gameOverChecker.gameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
        SoundManager.instance.backgroundMusic.Stop();
        SoundManager.instance.PlayMusic(winMusic);
        winSound.Play();
    }
    //Random generate new tiles
    void Generate()
    {
        if(emptyTiles.Count > 0)
        {
            int indexForNewNunmber = Random.Range(0,emptyTiles.Count);
            //int randomNum = Random.Range(0,10);
            //if (randomNum == 0)
            //    emptyTiles[indexForNewNunmber].Number = 4;
            //else
            //emptyTiles[indexForNewNunmber].Number = 2;
            emptyTiles[indexForNewNunmber].Number = 2;
            emptyTiles[indexForNewNunmber].PlayAppearAnimation();
            emptyTiles.RemoveAt(indexForNewNunmber);
            //Debug.Log("It runs here in Generate");
        }
    }

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
        moveMade = false;
        ResetMergedFlags();
        if (delay > 0)
            StartCoroutine(MoveCoroutine(md));
        else
        {
            for (int i = 0; i < rows.Count; i++)
            {
                switch (md)
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
        }
 
        if (moveMade)
        {
            //To check whether there is en empty blcok
            UpdateEmptyTiles();
            //Create one tile after each move
            Generate();
            GameOver();
        }
    }

    IEnumerator MoveCoroutine(MoveDirection md)
    {
        State = GameState.WaitingForMoveToEnd;
        switch (md)
        {
            case MoveDirection.Down:
                for (int i = 0; i < columns.Count; i++)
                    StartCoroutine(MakeOneLineUpIndexCoroutine(columns[i], i));
                break;
            case MoveDirection.Up:
                for (int i = 0; i < columns.Count; i++)
                    StartCoroutine(MakeOneLineDownIndexCoroutine(columns[i], i));
                break;
            case MoveDirection.Left:
                for (int i = 0; i < rows.Count; i++)
                    StartCoroutine(MakeOneLineDownIndexCoroutine(rows[i], i));
                break;
            case MoveDirection.Right:
                for (int i = 0; i < rows.Count; i++)
                    StartCoroutine(MakeOneLineUpIndexCoroutine(rows[i], i));
                break;
        }

        //Wait until the move is over in all lines
        while (!(lineMoveComplete[0] && lineMoveComplete[1] &&
            lineMoveComplete[2] && lineMoveComplete[3]))
            yield return null;

        if (moveMade)
        {
            UpdateEmptyTiles();
            Generate();
            if (!TileCanMove())
                GameOver();
        }
        State = GameState.Playing;
        StopAllCoroutines();
    }

    IEnumerator MakeOneLineUpIndexCoroutine(Tile[] line,int index)
    {
        lineMoveComplete[index] = false;
        while (MakeOneMoveUpIndex(line))
        {
            moveMade = true;
            yield return new WaitForSeconds(delay);
        }
        lineMoveComplete[index] = true;
    }

    IEnumerator MakeOneLineDownIndexCoroutine(Tile[] line, int index)
    {
        lineMoveComplete[index] = false;
        while (MakeOneMoveDownIndex(line))
        {
            moveMade = true;
            yield return new WaitForSeconds(delay);
        }
        lineMoveComplete[index] = true;
    }



    public bool TileCanMove()
    {
        if(emptyTiles.Count > 0)
        {
            return true;
        }
        else
        {
            for (int rowPos = 0; rowPos < rows.Count; rowPos++)
            {
                for (int colPos = 0; colPos < columns.Count - 1; colPos++)
                {
                    if (alltiles[rowPos, colPos].Number == alltiles[rowPos, colPos + 1].Number)
                        return true;
                }
            }

            for (int colPos = 0; colPos < columns.Count; colPos++){
                for(int rowPos = 0; rowPos < rows.Count -1; rowPos++)
                {
                    if(alltiles[rowPos, colPos].Number == alltiles[rowPos + 1, colPos].Number)
                        return true;
                }
            }
        }
        return false;
    }

    void GameOver()
    {
        //if (YouWin())
        //{
        //    gameOverChecker.transform.gameObject.SetActive(true);
        //    gameOverChecker.transform.GetChild(0).gameObject.SetActive(false);
        //    gameOverChecker.gameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
        //}

        if (!TileCanMove())
        {
            gameOverChecker.transform.gameObject.SetActive(true);
            gameOverChecker.transform.GetChild(1).gameObject.SetActive(false);
            gameOverChecker.gameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
            SoundManager.instance.PlayMusic(lostMusic);
            lostsound.Play();
            SoundManager.instance.backgroundMusic.Stop();
            State = GameState.GameOver;
        }
    }
    public void NewGameButtonHandler()
    {
       
        foreach (Tile t in alltiles)
            t.Number = 0;
        //Creat two tiles
        Generate();
        Generate();
        ScoreTracker.Instance.Score = 0;
        SoundManager.instance.PlayMusic(moveSound);
    }

    public void RestartANewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
