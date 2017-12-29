using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICooksFood
{
    int Capacity { get; }

    void HeatUp();
    void Reheat();

}
