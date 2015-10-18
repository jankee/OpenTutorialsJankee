using UnityEngine;
using System.Collections;

public class CombatTextManager : MonoBehaviour 
{
    private static CombatTextManager instance;
    public static CombatTextManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManager>();
            }
            return instance; 
        }
    }

    public GameObject TextObj;

    public void CreateText(Vector3 position)
    {
        Instantiate(TextObj, position, Quaternion.identity);

        TextObj.transform.parent = GameObject.Find("Canvas").transform;
 
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
