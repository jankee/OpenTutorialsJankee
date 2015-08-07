using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using common;

public class GameManager : MonoBehaviour 
{
    public BgCellDisplayer bgGrid;
    public Camera mainCamera;

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

    void Awake()
    {
        _instance = this;
        init();
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
        unitList.Add(unit);
    }

    Point getStartPoint()
    {
        List<Point> startPointList = new List<Point>();

        int _w = wallMap.GetLength(0);
        int _h = wallMap.GetLength(1);
        int x, y;

        for (x = 0; x < _h; x++)
        {
            for (y = 0; y < _h; y++)
            {
                startPointList.Add(new Point(x, y));
            }   
        }

        if (startPointList.Count == 0)
        {
            print("Not Found Start Position");
            return null;
        }

        int ranIdx = Random.Range(0, startPointList.Count);
        return startPointList[ranIdx];
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

}
