using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D[] mapData;

    [SerializeField]
    private MpaElement[] mpaElements;

    [SerializeField]
    private Sprite defaultTile;

    public Vector3 WorldStartPos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateMap()
    {
        for (int i = 0; i < mapData.Length; i++)
        {
            for (int x = 0; x < mapData[i].width; x++)
            {
                for (int y = 0; y < mapData[i].height; y++)
                {

                }
            }
        }
        
    }
}

[Serializable]
public class MpaElement
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