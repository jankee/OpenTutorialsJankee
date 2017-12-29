using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStingPatrol : IWorker
{
    int AlertLevel { get; }
    int StingerLength { get; }
    bool LookForEnemies();
    int SharpenStinger(int length);
}
