using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorker
{
    string Job { get; }
    int Left { get; }
    void DoThisJob(string Job, int Shifts);
    void WorkOneShift();
}
