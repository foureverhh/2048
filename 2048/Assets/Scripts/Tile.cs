using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    public bool mergedThisTurn = false;
    public int indexRow;
    public int indexCol;

    public int Number
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            if(number == 0)
                SetEmpty();
            else
            {
                ApplyStyle(number);
                SetVisible();
            }
        }
    }

    private int number;

    private Text tileText;
    private Image tileImage;

    private void Awake()
    {
        tileText = GetComponentInChildren<Text>();
        tileImage = transform.GetChild(0).GetComponent<Image>();

        //tileImage = transform.Find("NumberedCell").GetComponent<Image>();
        //Debug.Log("Image is: " + tileImage.gameObject.name);
    }

    void ApplyStyleFromHolder(int index)
    {
        tileText.text = TilestyleHolder.Instance.tileStyles[index].Number.ToString();
        tileText.color = TilestyleHolder.Instance.tileStyles[index].NumberColor;
        tileImage.color = TilestyleHolder.Instance.tileStyles[index].TileColor;
    }

    void ApplyStyle(int number)
    {
        switch(number)
        {
            case 2:
                ApplyStyleFromHolder(0);
                break;
            case 4:
                ApplyStyleFromHolder(1);
                break;
            case 8:
                ApplyStyleFromHolder(2);
                break;
            case 16:
                ApplyStyleFromHolder(3);
                break;
            case 32:
                ApplyStyleFromHolder(4);
                break;
            case 64:
                ApplyStyleFromHolder(5);
                break;
            case 128:
                ApplyStyleFromHolder(6);
                break;
            case 256:
                ApplyStyleFromHolder(7);
                break;
            case 512:
                ApplyStyleFromHolder(8);
                break;
            case 1024:
                ApplyStyleFromHolder(9);
                break;
            case 2048:
                ApplyStyleFromHolder(10);
                break;
            case 4096:
                ApplyStyleFromHolder(11);
                break;
            default:
                Debug.Log("Check the number that you pass to ApplyStyle");
                break;
        }
    }

    private void SetVisible()
    {
        tileImage.enabled = true;
        tileText.enabled = true;
    }

    private void SetEmpty()
    {
        tileImage.enabled = false;
        tileText.enabled = false;
    }

 
	
	// Update is called once per frame
	void Update () {
		
	}
}
