using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour 
{
    public GameObject leftTilePrefab;
    
    public GameObject currentTilePrefab;


	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnTile();    
        }
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void SpawnTile()
    {
        currentTilePrefab = (GameObject)Instantiate(leftTilePrefab, currentTilePrefab.transform.GetChild(0).transform.GetChild(0).position, Quaternion.identity);
    }
}
