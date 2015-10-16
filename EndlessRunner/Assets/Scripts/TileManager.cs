using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour 
{
    public GameObject[] tilePrefabs;
    
    public GameObject currentTilePrefab;

    private Stack<GameObject> leftTiles = new Stack<GameObject>();

    public Stack<GameObject> LeftTiles
    {
        get { return leftTiles; }
        set { leftTiles = value; }
    }

    private Stack<GameObject> topTiles = new Stack<GameObject>();

    public Stack<GameObject> TopTiles
    {
        get { return topTiles; }
        set { topTiles = value; }
    }

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
        CreateTiles(50);

        for (int i = 0; i < 50; i++)
        {
            SpawnTile();
        }
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
            leftTiles.Peek().name = "LeftTile";
            leftTiles.Peek().SetActive(false);

            topTiles.Peek().name = "TopTile";
            topTiles.Peek().SetActive(false);
        }

    }

    public void SpawnTile()
    {
        if (leftTiles.Count == 0 || topTiles.Count == 0)
        {
            CreateTiles(10);
        }

        //0과 1사이의 랜덤값을 생성한다
        int randomIndex = Random.Range(0, 2);

        if (randomIndex == 0)
        {
            GameObject tmp = leftTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTilePrefab.transform.GetChild(0).transform.GetChild(randomIndex).transform.position;
            currentTilePrefab = tmp;

        }
        else if (randomIndex == 1)
        {
            GameObject tmp = topTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTilePrefab.transform.GetChild(0).transform.GetChild(randomIndex).transform.position;
            currentTilePrefab = tmp;
        }

        int spawnRandom = Random.Range(0, 10);

        if (spawnRandom == 0)
        {
            currentTilePrefab.transform.GetChild(1).gameObject.SetActive(true);
        }

        //currentTilePrefab = (GameObject)Instantiate(tilePrefabs[randomIndex], currentTilePrefab.transform.GetChild(0).transform.GetChild(randomIndex).position, Quaternion.identity);
    }

    public void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
