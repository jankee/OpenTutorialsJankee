using System.Collections;
using System.Collections.Generic;
using common;
using UnityEngine;


public class PathFinder
{
    private static PathFinder _instance = null;
    private MapData mapData;
    private int[,] orgMap;
    private bool isSearching = false;
    private int searchCount = 0;
    private Point goalPoint;

    public static PathFinder instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = new PathFinder();
            }
            return _instance; 
        }
    }
    
    public PathFinder()
    {
        init();
    }


    public void init()
    {
        mapData = new MapData();
        isSearching = false;
        searchCount = 0;
    }

    public void setMapData(int[,] _map)
    {
        orgMap = _map;
        mapData.setMapData(orgMap);
    }

    public Point[] getPath(Point current, int targetInx)
    {
        if (!checkInMapPoint(current) || !checkInMapIndex(targetInx))
        {
            return null;
        }

        mapData.setMapData(orgMap);
        Debug.Log("START SEARCHING.....");
        isSearching = true;
        searchCount = 1;
        setPathCount(new List<Point>() { current }, targetInx, 100);
        List<Point> pList = findPath();
        if (pList == null)
        {
            return null;
        }
        return pList.ToArray();
    }

    private void setPathCount(List<Point> pList, int targetIdx)
    {
        if (orgMap == null)
        {
            return;
        }

        setPathCount(pList, targetIdx, orgMap.GetLength(0) * orgMap.GetLength(1));
    }

    private void setPathCount(List<Point> pList, int targetIdx, int max)
    {
        if (!isSearching)
        {
            return;
        }
        List<Point> _List = new List<Point>();

        foreach (Point p in pList)
        {
            List<Point> retList = recordPath(p, targetIdx);
            if (retList != null && retList.Count > 0)
            {
                _List.AddRange(retList);
            }
        }
        if (_List.Count == 0)
        {
            Debug.Log("SEARCHING FAIL......!");
        }
        searchCount++;
        if (searchCount >= max)
        {
            isSearching = false;
            return;
        }

        setPathCount(_List, targetIdx, max);
    }

    private List<Point> recordPath(Point p, int targetIdx)
    {
        

        if (!isSearching)
        {
            return null;
        }
        
        if (orgMap[p.x, p.y] == targetIdx)
        {
            isSearching = false;
            mapData.map[p.x, p.y] = searchCount;
            goalPoint = p;
            Debug.Log("SEARCHING COMPLETE...!: " + searchCount);
            return null;
        }

        List<Point> rList = null;

        if (mapData.map[p.x, p.y] == 0)
        {
            mapData.map[p.x, p.y] = searchCount;
            rList = new List<Point>();
            getNeighbours(p, ref rList);
        }

        return rList;
    }

    private void getNeighbours(Point p, ref List<Point> rList)
    {
        if (p.x > 0 && mapData.map[p.x - 1, p.y] == 0)
        {
            rList.Add(new Point(p.x - 1, p.y));
        }
        if (p.y > 0 && mapData.map[p.x, p.y - 1] == 0)
        {
            rList.Add(new Point(p.x, p.y - 1));
        }

        if (p.x < orgMap.GetLength(0) - 1 && mapData.map[p.x + 1, p.y] == 0)
        {
            rList.Add(new Point(p.x + 1, p.y));
        }
        if (p.y < orgMap.GetLength(1) - 1 && mapData.map[p.x, p.y + 1] == 0)
        {
            rList.Add(new Point(p.x, p.y + 1));
        }
    }

    private List<Point> findPath()
    {
        List<Point> pathList = new List<Point>();
        Point temPoint = goalPoint;
        int _count = searchCount - 1;

        while (_count > 0)
        {
            pathList.Insert(0, temPoint.clone());
            getPathPoint(ref temPoint, _count);
            _count--;
        }
        pathList.Insert(0, temPoint);
        if (isSearching)
        {
            isSearching = false;
            return null;
        }
        return pathList;
    }

    private void getPathPoint(ref Point p, int count)
    {
        if (p.y < mapData.map.GetLength(1) - 1 && mapData.map[p.x, p.y + 1] == count)
        {
            p.y += 1;
            return;
        }
        if (p.x < mapData.map.GetLength(0) - 1 && mapData.map[p.x + 1, p.y] == count)
        {
            p.x += 1;
            return;
        }
        if (p.y < 0 && mapData.map[p.x, p.y - 1] == count)
        {
            p.y -= 1;
            return;
        }
        if (p.x < 0 && mapData.map[p.x - 1, p.y] == count)
        {
            p.x -= 1;
            return;
        }
    }


	private bool checkInMapPoint(Point p)
    {
        if (orgMap == null)
        {
            return false;
        }
        if (p.x < 0 || p.y < 0)
        {
            return false;
        }
        if (p.x >= orgMap.GetLength(0) || p.y >= orgMap.GetLength(1))
        {
            return false;
        }

        return true;
    }

    private bool checkInMapIndex(int idx)
    {
        if (orgMap == null)
        {
            return false;
        }
        foreach (int _idx in orgMap)
        {
            if (idx == _idx)
            {
                return true;
            }
        }
        return false;
    }
}
