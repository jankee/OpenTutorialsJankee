using UnityEngine;
using System.Collections;

public class Queen : MonoBehaviour 
{
    private Worker[] workers;
    public Queen(Worker[] workers)
    {
        this.workers = workers;


        print(WorkTheNextShift());

        WorkTheNextShift();
    }

    private int shiftNumber = 0;

    public bool AssingWork(string job, int numberOfShifts)
    {
        for (int i = 0; i < workers.Length; i++)
        {
            if (workers[i].DoThisJob(job, numberOfShifts))
            {
                return true;
            }                
        }

        return false;
    }

    public string WorkTheNextShift()
    {
        shiftNumber++;

        print(workers.Length);

        string report = "Report for shift #" + shiftNumber + " \r\n";
        for (int i = 0; i < workers.Length; i++)
        {
            if (workers[i].WorkOneShift())
            {
                report += "Worker #" + (i + 1) + " finished the job\n";
            }
            if (string.IsNullOrEmpty(workers[i].Currentjob))
            {
                report += "Worker #" + (i + 1) + "is not working\n";
            }
            else
            {
                if (workers[i].ShiftsLeft > 0)
                {
                    report += "Worker #" + (i + 1) + " is doing '" + workers[i].Currentjob + "' for " + workers[i].ShiftsLeft + " more shifts\n";
                }
                else
                {
                    report += "Worker #" + (i + 1) + " will be done with '" + workers[i].Currentjob + "' after this shift\n";
                }
            }
        }

        return report;
    }
}
