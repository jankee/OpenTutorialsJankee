using UnityEngine;
using System.Collections;

public class BgCell : MonoBehaviour 
{
    public Color white = Color.white;
    public Color black = Color.black;

    public Color startCol = Color.green;
    public Color goalCol = Color.blue;

    private tk2dSlicedSprite _spr;
    private tk2dSlicedSprite spr
    {
        get
        {
            if (_spr == null)
            {
                _spr = this.GetComponentInChildren<tk2dSlicedSprite>();
            }
            return _spr;
        }
    }

    public bool isVisable
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

    public void setBlack(bool isBlack)
    {
        if (isBlack)
        {
            spr.color = black;
        }
        else
        {
            spr.color = white;
        }
    }

    public void setStart()
    {
        spr.color = startCol;
    }

    public void setGoal()
    {
        spr.color = goalCol;
    }

}
