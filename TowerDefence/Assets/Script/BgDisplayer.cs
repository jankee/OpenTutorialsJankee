using UnityEngine;
using System.Collections;

public class BgDisplayer : MonoBehaviour 
{
    public Bg cellTile;

    private Bg[,] cellTileArr;
    private GameManager gm;

	// Use this for initialization
	void Start () 
    {
        gm = GameManager.instance;
        cellTileArr = new Bg[0, 0];
        showBgCellTile();
        Debug.Log(gm.wallMap.GetLength(0));
        Debug.Log(gm.wallMap.GetLength(1));
        Debug.Log(gm.wallMap.Length);
	}
	
	// Update is called once per frame
	public void showBgCellTile () 
    {
        //wallMap의 20개의 2차원 배열을 length에 넣어 준다.
        int length = gm.wallMap.GetLength(0);
        //wallMap의 13개 배열을 height에 넣어 준다.
        int height = gm.wallMap.GetLength(1);

        //cellTileArr에 wallMap의 배열로 만들어 준다.
        cellTileArr = new Bg[length, height];

        int x, y;

        for (x = 0; x < length; x++)
        {
            for (y = 0; y < height; y++)
            {
                Bg bgCell = Instantiate(cellTile) as Bg;
                bgCell.transform.parent = this.transform;
                bgCell.transform.localPosition = new Vector3(40 * x, -40 * y, 0);
                cellTileArr[x, y] = bgCell;
                cellTileArr[x, y].name = "Cell" + x + y; 
                print("Cell : " + cellTileArr[x, y].name);
            }   
        }
        refreshCellDisplay();
	}

    public void refreshCellDisplay()
    {
        //wallMap의 20개의 2차원 배열을 length에 넣어 준다.
        //int _w = gm.wallMap.GetLength(0);
        //wallMap의 13개 배열을 height에 넣어 준다.
        //int _h = gm.wallMap.GetLength(1);

        int x, y;

        for (x = 0; x < 20; x++)
        {
            for (y = 0; y < 13; y++)
            {
                Bg bgCell = cellTileArr[x, y];

                // 10이하로는 setActivete를 꺼준다.
                if (gm.wallMap[x, y] < 10 && gm.wallMap[x, y] != 0)
                {
                    bgCell.isVisableBg = true;
                }
                // 11이면 스타트 컬러를 대입
                else if (gm.wallMap[x, y] == 11)
                {
                    bgCell.isVisableBg = true;
                    bgCell.setStart();
                }
                // 111이면 골 컬러로 대입
                else if (gm.wallMap[x, y] == 111)
                {
                    bgCell.isVisableBg = true;
                    bgCell.setGoal();
                }
                else if (gm.wallMap[x, y] == 2)
                {
                    bgCell.isVisableBg = true;   
                }
                else
                {
                    bgCell.isVisableBg = true;
                    bool isBlack = (x + (y % 2)) % 2 == 1;
                    bgCell.setBlack(isBlack);
                }

            }   
        }
    }

    public void textTest()
    {
        Debug.Log("Test");
                
        int x, y;

        for (x = 0; x < 20; x++)
        {
            for (y = 0; y < 13; y++)
            {
                print("Cell : " + cellTileArr[x, y].name);

            }   
        }
    }
}
