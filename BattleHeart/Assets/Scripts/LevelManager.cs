using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform map;

    [SerializeField]
    private Texture2D[] mapData;

    [SerializeField]
    private MapElement[] mapElements;

    [SerializeField]
    private Sprite defaultTile;

    public Vector2 WorldStartPos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        }
    }


    // Use this for initialization
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateMap()
    {
        int width = mapData[0].width;
        int height = mapData[0].height;

        for (int i = 0; i < mapData.Length; i++)
        {
            for (int x = 0; x < mapData[i].width; x++)
            {
                for (int y = 0; y < mapData[i].height; y++)
                {
                    Color colorData = mapData[i].GetPixel(x, y);

                    print(colorData);

                    MapElement newElement = Array.Find(mapElements, e => e.MyColor == colorData);

                    if (newElement != null)
                    {
                        float xPos = WorldStartPos.x + (defaultTile.bounds.size.x * x);
                        float yPos = WorldStartPos.y + (defaultTile.bounds.size.y * y);

                        GameObject go = Instantiate(newElement.MyElementPrefab);

                        go.transform.position = new Vector2(xPos, yPos);

                        if (newElement.MyElementPrefab.tag == "Obstacle")
                        {
                            go.GetComponent<SpriteRenderer>().sortingOrder = width * 2 - height * 2;
                        }

                        go.transform.SetParent(map);
                    }
                }
            }
        }
        
    }
}

[Serializable]
public class MapElement
{
    [SerializeField]
    private string tileTag;

    [SerializeField]
    private Color color;

    [SerializeField]
    private GameObject elementPrefab;

    

    public Color MyColor
    {
        get
        {
            return color;
        }
    }

    public string MyTileTag
    {
        get
        {
            return tileTag;
        }
    }

    public GameObject MyElementPrefab
    {
        get
        {
            return elementPrefab;
        }
    }
}