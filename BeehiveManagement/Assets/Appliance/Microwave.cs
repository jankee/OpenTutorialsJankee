using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : Appliance, ICooksFood
{
    int ICooksFood.Capacity
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    void ICooksFood.HeatUp()
    {
        throw new System.NotImplementedException();
    }

    void ICooksFood.Reheat()
    {
        throw new System.NotImplementedException();
    }

    public void MakePopcorn()
    {

    }

}
