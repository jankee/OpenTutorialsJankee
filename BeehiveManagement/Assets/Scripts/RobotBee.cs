using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBee : Robot, IWorker
{
    private string job;
    public string Job
    {
        get
        {
            return job;
        }
    }

    private int left;
    public int Left
    {
        get
        {
            return left;
        }
    }

    void IWorker.DoThisJob(string Job, int Shifts)
    {
        throw new System.NotImplementedException();
    }

    void IWorker.WorkOneShift()
    {
        throw new System.NotImplementedException();
    }
}
