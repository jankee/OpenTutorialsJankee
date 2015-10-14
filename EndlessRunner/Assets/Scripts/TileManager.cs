using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour 
{
    public GameObject[] tilePrefabs;
    
    public GameObject currentTilePrefab;

    private Stack<GameObject> leftTiles = new Stack<GameObject>();

    private Stack<GameObject> topTiles = new Stack<GameObject>();

    private static TileManager instance;

    public static TileManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TileManager>();
            }

            return instance; 
        }
    }

	// Use this for initialization
	void Start () 
    {
        CreateTiles(25);
	}

	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void CreateTiles(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            leftTiles.Push(Instantiate(tilePrefabs[0]));
            topTiles.Push(Instantiate(tilePrefabs[1]));
        }

        print(leftTiles.Count);
        print(topTiles.Count);
    }

    public void SpawnTile()
    {
        //0과 1사이의 랜덤값을 생성한다
        int randomIndex = Random.Range(0, 2);

        currentTilePrefab = (GameObject)Instantiate(tilePrefabs[randomIndex], currentTilePrefab.transform.GetChild(0).transform.GetChild(randomIndex).position, Quaternion.identity);
    }
}
