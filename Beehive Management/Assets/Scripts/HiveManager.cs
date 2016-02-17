using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HiveManager : MonoBehaviour 
{
    private Worker[] workers = new Worker[4];

    private Queen queen;

    public Button workerBeeJob;

    public static bool checkButton = false;

	// Use this for initialization
	void Start () 
    {
        workers[0] = new Worker(new string[] { "Nectar collector", "Honey manufacturing" });
        workers[1] = new Worker(new string[] { "Egg care", "Baby bee tutoring" });
        workers[2] = new Worker(new string[] { "Hive maintenance", "Sting patrol" });
        workers[3] = new Worker(new string[] { "Nectar collector", "Honey manufacturing", "Egg care", "Baby bee tutoring", "Hive maintenance", "Sting patrol" });

        queen = new Queen(workers);
	}
	
    public void AssignJob()
    {
        print(workerBeeJob.GetComponentInChildren<Text>().text);

        queen.AssingWork("HI", 3);

        //if (queen.AssingWork()
        //{
            
        //}
    }

	// Update is called once per frame
	void Update () {
	
	}
}
