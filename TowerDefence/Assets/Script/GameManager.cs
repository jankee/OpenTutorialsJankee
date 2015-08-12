using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using common;

public class GameManager : MonoBehaviour 
{


    private static GameManager _instance = null;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                print("GameManager is NULL");
            }
            return _instance;
        }
    }

    public float cellSize = 40.0f;

    public int[,] wallMap =
       {{1, 1, 1, 1, 1, 11, 11, 11, 1, 1, 1, 1, 1},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {1, 1, 1, 1, 1, 111, 111, 111, 1, 1, 1, 1, 1}};

    [HideInInspector]
    public List<UnitBase> unitList = new List<UnitBase>();
    public Transform unit_field;
    public UnitBase unit_marine;
    public BgDisplayer bgGrid;
    public Camera mainCamera;

    void Awake()
    {
        bgGrid = new BgDisplayer();
        _instance = this;
        init();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Input.mousePosition;
            Debug.Log("mouse : " + pos);
            Debug.Log("Screen : " + mainCamera.ScreenToViewportPoint(pos));
            Debug.Log("unit_field : " + unit_field.TransformPoint(Vector3.zero));
            Vector3 mouseP = mainCamera.ScreenToWorldPoint(pos) - unit_field.InverseTransformPoint(Vector3.zero);
            Point myPos = new Point(-(int)(mouseP.x / cellSize), (int)(mouseP.y / cellSize));
            Debug.Log(myPos.x + " " + myPos.y);
            switchWall(myPos);
        }

    }

    private void init()
    {
        unitList.Clear();

        initPathFinder();
    }

    void addUnit()
    {
        UnitBase unit = Instantiate(unit_marine) as UnitBase;
        unit.transform.parent = unit_field;
        Point startPoint = getStartPoint();
        unit.transform.localPosition = new Vector3(startPoint.x * cellSize + cellSize / 2.0f, -startPoint.y * cellSize - cellSize / 2.0f);
        unit.setStartPoint(startPoint);
    }

    public void removeUnit(UnitBase ub)
    {
        unitList.Remove(ub);
    }

    void researchPathUnits()
    {
        foreach (UnitBase ub in unitList)
        {
            if (ub != null)
            {
                ub.getPath();
            }
        }
    }

    Point getStartPoint()
    {
        List<Point> startPointList = getStartPointList();

        if (startPointList.Count == 0)
        {
            Debug.Log("Not Found Start Position");
            return null;
        }
        int ranIdx = Random.Range(0, startPointList.Count);
        return startPointList[ranIdx];
    }

    List<Point> getStartPointList()
    {
        List<Point> startPointList = new List<Point>();
        int _w = wallMap.GetLength(0);
        int _h = wallMap.GetLength(1);
        int x, y;

        for (x = 0; x < _h; x++)
        {
            for (y = 0; y < _h; y++)
            {
                if (wallMap[x, y] >= 10 && wallMap[x, y] <= 100)
                {
                    startPointList.Add(new Point(x, y));    
                }
                
            }
        }
        return startPointList;
    }

    public void initPathFinder()
    {
        PathFinder.instance.setMapData(wallMap);
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "Add Unit"))
        {
            addUnit();
        }
    }

    bool checkResearchAble(Point p)
    {
        foreach (Point sp in getStartPointList())
        {
            if (PathFinder.instance.getPath(sp, 100 + wallMap[sp.x, sp.y]) == null)
            {
                Debug.Log("StartPoint Path NULL");
                return false;
            }
        }
        foreach (UnitBase unit in unitList)
        {
            if (!unit.getPath())
            {
                Debug.Log("Uni Path NULL");
                return false;
            }
        }
        return true;
    }

    void switchWall(Point p)
    {
        if (p.x < 0 || p.y >= wallMap.GetLength(0) || p.y >= wallMap.GetLength(1))
        {
            
            return;
        }

        print("switchWall p :" + p.x + p.y);
        int prevIndex = wallMap[p.x, p.y];

        if (wallMap[p.x, p.y] == 0)
        {
            wallMap[p.x, p.y] = 2;
            print("switchWall p :" + wallMap[p.x, p.y]);
        }
        else if (wallMap[p.x, p.y] == 2)
        {
            wallMap[p.x, p.y] = 0;
        }

        if (checkResearchAble(p))
        {
            //bgGrid.textTest();
            bgGrid.refreshCellDisplay();
            researchPathUnits();
        }
        else
        {
            wallMap[p.x, p.y] = prevIndex;
        }
    }
}
