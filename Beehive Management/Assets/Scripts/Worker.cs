using UnityEngine;
using System.Collections;

public class Worker : MonoBehaviour 
{
    private string curentjob = "";
    private string[] jobsICanDo;

    public Worker(string[] jobsICanDo)
    {
        this.jobsICanDo = jobsICanDo;
        print("hi" + jobsICanDo);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
