using UnityEngine;
using System.Collections;

public class Queen : MonoBehaviour 
{
    public Worker[] workers = new Worker[4];
    


	// Use this for initialization
	void Start () 
    {
        workers[0] = new Worker(new string[] { "Nectar collector", "Honey manufacturing" }); 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
