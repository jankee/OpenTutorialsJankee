using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour 
{
    float delayTime = 0.5f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            TileManager.Instance.SpawnTile();
            StartCoroutine("FallDown");

            //PlayerScript.Instance.speed += -0.1f;
        }
    }

    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(delayTime);
        GetComponent<Rigidbody>().isKinematic = false;

        yield return new WaitForSeconds(2);

        switch (gameObject.name)
        {
            case "LeftTile":
                TileManager.Instance.LeftTiles.Push(gameObject);
                GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(true);
                break;

            case "TopTile":
                TileManager.Instance.TopTiles.Push(gameObject);
                GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(true);
                break;
        }
    }
}
