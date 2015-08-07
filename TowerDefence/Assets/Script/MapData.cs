using System.Collections;
using System.Collections.Generic;
using common;
using Debug = UnityEngine.Debug;

public class MapData
{
    public int[,] map;
    public MapData()
    {

    }

    public void setMapData(int[,] _map)
    {
        int w = _map.GetLength(0);
        int h = _map.GetLength(1);

        map = new int[w, h];

        int x, y;

        for (x = 0; x < w; x++)
        {
            for (y = 0; y < h; y++)
            {
                if (_map[x, y] > 0 && _map[x, y] < 10)
                {
                    map[x, y] = -1;
                }
                else
                {
                    map[x, y] = 0;
                }
            }   
        }
    }
}
