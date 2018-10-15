using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileStyle
{
    public int Number;
    public Color32 TileColor;
    public Color32 NumberColor;
}

public class TilestyleHolder : MonoBehaviour {

    //SINGELTON
    public static TilestyleHolder Instance;

    public TileStyle[] tileStyles;

    private void Awake()
    {
        Instance = this;
    }


}
