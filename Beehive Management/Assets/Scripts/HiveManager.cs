using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HiveManager : MonoBehaviour 
{
    private Worker[] workers = new Worker[4];

    private Queen queen;

    public Button workerBeeJob;


    public InputField shifts;

    public Text report;

    public static bool checkButton = false;

	// Use this for initialization
	void Start () 
    {
        workers[0] = new Worker(new string[] { "Nectar collector", "Honey manufacturing" }, 175);
        workers[1] = new Worker(new string[] { "Egg care", "Baby bee tutoring" }, 114);
        workers[2] = new Worker(new string[] { "Hive maintenance", "Sting patrol" }, 149);
        workers[3] = new Worker(new string[] { "Nectar collector", "Honey manufacturing", "Egg care", "Baby bee tutoring", "Hive maintenance", "Sting patrol" }, 155);

        queen = new Queen(workers);

        workerBeeJob.transform.FindChild("Container").gameObject.SetActive(true);
	}

    public void AssignJob()
    {
        //print(queen.AssingWork(workerBeeJob.GetComponentInChildren<Text>().text, int.Parse(shifts.text)));

        if (queen.AssingWork(workerBeeJob.GetComponentInChildren<Text>().text, int.Parse(shifts.text)) == false)
        {
            report.text = "No workers are available to do the job '" + workerBeeJob.GetComponentInChildren<Text>().text + "'\n" + "The queen bee says....";
        }
        else
        {
            report.text = "The job '" + workerBeeJob.GetComponentInChildren<Text>().text + "' will be done in " + shifts.text + " shifts\n" + "The queen bee says....";
        }

        //자식을 찾을 때
        //Button dropDown = transform.FindChild("DropDown");

    }

    public void nextShift()
    {
        report.text = queen.WorkTheNextShift();
    }
}
