using UnityEngine;
using System.Collections;

public class Queen : Bee
{
    private Worker[] workers;

    private int shiftNumber = 0;

    //여왕벌 생성자
    public Queen(Worker[] workers) : base(275)
    {
        this.workers = workers;

        //WorkTheNextShift();
    }

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
        double totalConsumption = 0;
        
        //각 벌들의 꿀 소비량을 더함
        for (int i = 0; i < workers.Length; i++)
        {
            totalConsumption += workers[i].GetHoneyConsumption();
        }
        //여왕의 꿀 소비량까지 더함
        totalConsumption += GetHoneyConsumption();

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
                if (workers[i].shiftsLeft > 0)
                {
                    report += "Worker #" + (i + 1) + " is doing '" + workers[i].Currentjob + "' for " + workers[i].shiftsLeft + " more shifts\n";
                }
                else
                {
                    report += "Worker #" + (i + 1) + " will be done with '" + workers[i].Currentjob + "' after this shift\n";
                }
            }
        }

        report += "Total Honey consumption : " + totalConsumption + " units";

        return report;
    }

    public override double GetHoneyConsumption()
    {
        double consumption = 0;
        double largestWorkerConsumption = 0;
        int workersDoingJobs = 0;

        for (int i = 0; i < workers.Length; i++)
        {
            if (workers[i].GetHoneyConsumption() > largestWorkerConsumption)
            {
                largestWorkerConsumption = workers[i].GetHoneyConsumption();
            }

            //일하고 있는 벌들을 찾는다
            if (workers[i].shiftsLeft > 0)
            {
                workersDoingJobs++;
            }
        }
        //가장 길게 일한는 벌에 소비량을 넣어 준다.
        consumption += largestWorkerConsumption;

        //일하고 있는 벌이 3명 이상일 때 여왕의 소비량에 30, 3명이하면 20을 더 한다.
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
