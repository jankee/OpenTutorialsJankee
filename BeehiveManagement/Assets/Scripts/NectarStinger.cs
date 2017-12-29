using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarStinger : /*Worker, INectarCollector, IStingPatrol*/ MonoBehaviour
{
    //public NectarStinger():base(workType1, workType2, weight)
    //{
    //    //WorkType work = workType1;
    //}

    private int alertLevel;
    public int AlertLevel
    {
        get
        {
            return alertLevel;
        }
    }

    private int stingerLength;
    public int StingerLength
    {
        get
        {
            return stingerLength;
        }
        set
        {
            stingerLength = value;
        }
    }

    public string Job
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public int Left
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public bool LookForEnemies()
    {
        return true;
    }


    public int SharpenStinger(int length)
    {
        return length;
    }


    public void FindFlower()
    {
        throw new System.NotImplementedException();
    }

    //void IWorker.DoThisJob(string Job, int Shifts)
    //{
    //    throw new System.NotImplementedException();
    //}

    //void IWorker.WorkOneShift()
    //{
    //    throw new System.NotImplementedException();
    //}

    public void GatherNectar()
    {
        throw new System.NotImplementedException();
    }

    public void ReturnToHive()
    {
        throw new System.NotImplementedException();
    }
}
