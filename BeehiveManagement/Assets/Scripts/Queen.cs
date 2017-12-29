using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Bee
{
    private Worker[] workers;
    private int shiftNumber = 0;

    public Queen (Worker[] worker):base(275)
    {
        this.workers = worker;
    }

    //일벌들에게 일을 배정
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

    //다음 시간을 배정
    public string WorkTheNextShift()
    {
        float totalConsumption = 0;

        for (int i = 0; i < workers.Length; i++)
        {
            totalConsumption += workers[i].GetHoneyConsumption();
        }
        //자신의 소모량을 더해준다
        totalConsumption += GetHoneyConsumption();

        shiftNumber++;

        string report = "Report for shift #" + shiftNumber + "\n";

        for (int i = 0; i < workers.Length; i++)
        {
            if (workers[i].WorkOneShift())
            {
                report += "Worker #" + (i + 1) + " finished the job\n";
            }

            if (string.IsNullOrEmpty(workers[i].CurrentJob))
            {
                report += "Worker #" + (i + 1) + " is not working\n";
            }
            else
            {
                if (workers[i].ShiftsLeft > 0)
                {
                    report += "Worker #" + (i + 1) + " is doing '" + workers[i].CurrentJob + "' for " + workers[i].ShiftsLeft + " more shift\n";
                }
                else
                {
                    report += "Worker #" + (i + 1) + "Will be done with '" + workers[i].CurrentJob + "' after this shift\n";
                }
            }
        }

        report += "Total honey consumption : " + totalConsumption + " units";

        return report;
    }

    public override float GetHoneyConsumption()
    {
        float consumption = 0;

        float largestWorkerConsumption = 0;

        int workersDoingJobs = 0;

        for (int i = 0; i < workers.Length; i++)
        {
            if (workers[i].GetHoneyConsumption() > largestWorkerConsumption)
            {
                largestWorkerConsumption = workers[i].GetHoneyConsumption();
            }
            if (workers[i].ShiftsLeft > 0)
            {
                workersDoingJobs++;
            }
        }

        consumption += largestWorkerConsumption;

        if (workersDoingJobs >= 3)
        {
            consumption += 30;
        }
        else
        {
            consumption += 20;
        }

        return consumption;
    }
}
