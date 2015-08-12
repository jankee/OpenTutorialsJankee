using UnityEngine;
using System.Collections;

public class BgCellDisplayer : MonoBehaviour 
{
    public BgCell bgCell;

    private BgCell[,] bgCellArr;
    private GameManager gm;

	// Use this for initialization
	void Start () 
    {
        gm = GameManager.instance;
        showBgCells();
	}
	
	// Update is called once per frame
	public void showBgCells () 
    {
        
        int _w = gm.wallMap.GetLength(0);
        int _h = gm.wallMap.GetLength(1);
        bgCellArr = new BgCell[_w, _h];

        int x, y;

        for (x = 0; x < _w; x++)
        {
            for (y = 0; y < _h; y++)
            {
                BgCell bc = Instantiate(bgCell) as BgCell;
                bc.transform.parent = this.transform;
                bc.transform.localPosition = new Vector3(40 * x, -40 * y, 0);
                bgCellArr[x, y] = bc;
            }   
        }
        refreshDisplay();
	}

    public void refreshDisplay()
    {
        int _w = gm.wallMap.GetLength(0);
        int _h = gm.wallMap.GetLength(1);
        int x, y;

        for (x = 0; x < _w; x++)
        {
            for (y = 0; y < _h; y++)
            {
                BgCell bc = bgCellArr[x, y];

                if (gm.wallMap[x, y] < 10 && gm.wallMap[x, y] != 0)
                {
                    bc.isVisable = false;
                }
                else if (gm.wallMap[x, y] >= 10 && gm.wallMap[x, y] <= 100)
                {
                    bc.isVisable = true;
                    bc.setStart();
                }
                else if (gm.wallMap[x, y] >= 110)
                {
                    bc.isVisable = true;
                    bc.setGoal();
                }
                else
                {
                    bc.isVisable = true;
                    bool isBlack = ((x + (y % 2)) % 2 == 1);
                    bc.setBlack(isBlack);
                }
            }
        }
    }
}
