using UnityEngine;
using System.Collections;

public class BgCellDisplayer : MonoBehaviour 
{
    public BgCell bgCell;

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
        int x, y;

        for (x = 0; x < _w; x++)
        {
            for (y = 0; y < _h; y++)
            {
                BgCell bc = Instantiate(bgCell) as BgCell;
                bc.transform.parent = this.transform;
                bc.transform.localPosition = new Vector3(40 * x, -40 * y, 0);

                if (gm.wallMap[x, y] == 1)
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
