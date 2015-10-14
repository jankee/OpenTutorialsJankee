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
        }
    }

    IEnumerator FallDown()
    {
        print("HI");
        yield return new WaitForSeconds(delayTime);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
