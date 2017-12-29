using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WorkType
{
    NectarCollector,
    HoneyManufacturing,
    EggCare,
    BabyBeeTutoring,
    HiveMaintenance,
    StingPatrol,
}


public class GameManager : MonoBehaviour
{
    public Dropdown dropdown;
    public InputField inputField;
    public Button assignJob;
    public Button nextShift;

    public Text reportText;

    //드롭다운리스트
    private List<string> beeJobs = new List<string>() { "NectarCollector", "HoneyManufacturing", "EggCare", "BabyBeeTutoring", "HiveMaintenance", "StingPatrol", };
    public List<string> BeeJobs
    {
        get
        {
            return beeJobs;
        }
    }
    private string currentJob = "";
    private int currentShift = 0;

    private Worker[] worker;

    private Queen queen;

    

    void Awake()
    {
        //string[] jobs = BeeJobs.ToArray(); 

        worker = new Worker[4];
        worker[0] = new Worker(WorkType.NectarCollector, WorkType.HoneyManufacturing, 175f);
        worker[1] = new Worker(WorkType.EggCare, WorkType.BabyBeeTutoring, 114f);
        worker[2] = new Worker(WorkType.HiveMaintenance, WorkType.StingPatrol, 149f);
        worker[3] = new Worker(WorkType.NectarCollector, WorkType.BabyBeeTutoring, 155f);

        queen = new Queen(worker);

        dropdown.AddOptions(BeeJobs);
    }

    public void DropDownJobClick(int index)
    {
        currentJob = BeeJobs[index];
    }

    public void InputFieldShift()
    {
        currentShift = System.Convert.ToInt32(inputField.text);
    }

    public void AssignedJobToBee()
    {
        queen.AssingWork(currentJob, currentShift);

        print(currentJob + " : " + currentShift);
    }

    public void WorkNextShift()
    {
        reportText.text = queen.WorkTheNextShift();
    }
}
