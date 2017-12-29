using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Bee
{



    private string currentJob = "";
    public string CurrentJob
    {
        get
        {
            return currentJob;
        }
    }

    //Bee에 오버라이딩
    public override int ShiftsLeft
    {
        get
        {
            //일해야되는 시간에서 일한 시간을 빼준다.
            return shiftsToWork - shiftsWorked;
        }
    }

    public WorkType workType1;

    public WorkType workType2;

    private string[] jobsICanDo;

    public int shiftsToWork;
    public int shiftsWorked;

    private GameManager gameManager;

    //생성자
    public Worker(WorkType type1, WorkType type2, float weight):base(weight)
    {
        this.workType1 = type1;
        this.workType2 = type2;
        //this.jobsICanDo = job;
    }

    public bool DoThisJob(string job, int numberOfShifts)
    {
        if (!string.IsNullOrEmpty(currentJob))
        {
            return false;
        }

        if (workType1.ToString() == job || workType2.ToString() == job)
        {
            currentJob = job;
            this.shiftsToWork = numberOfShifts;
            shiftsWorked = 0;
            return true;
        }

        return false;
    }

    //시간단위 체크
    public bool WorkOneShift()
    {
        if (string.IsNullOrEmpty(currentJob))
        {
            return false;
        }

        shiftsWorked++;

        if (shiftsWorked > shiftsToWork)
        {
            shiftsWorked = 0;
            shiftsToWork = 0;
            currentJob = "";
            return true;
        }
        else
        {
            return false;
        }
    }

    public override float GetHoneyConsumption()
    {
        return base.GetHoneyConsumption();
    }
}
