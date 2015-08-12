using UnityEngine;
using System.Collections;

public class Bg : MonoBehaviour 
{
    public Color bgWhite = Color.white;
    public Color bgBlack = Color.black;

    public Color bgStartColor = Color.green;
    public Color bgGoalColor = Color.blue;

    private tk2dSlicedSprite bgTile;

    public tk2dSlicedSprite BgTile
    {
        get 
        {
            if (bgTile == null)
            {
                bgTile = this.GetComponentInChildren<tk2dSlicedSprite>();
            }
            return bgTile; 
        }
    }

    public bool isVisableBg
    {
        get
        {
            return this.gameObject.activeSelf;
        }
        set
        {
            this.gameObject.SetActive(value);
        }
    }

    public void setStart()
    {
        BgTile.color = bgStartColor;
    }

    public void setGoal()
    {
        BgTile.color = bgGoalColor;
    }
    public void setBlack(bool isBlack)
    {
        if (isBlack)
        {
            BgTile.color = bgBlack;
        }
        else
        {
            BgTile.color = bgWhite;
        }
    }
}
