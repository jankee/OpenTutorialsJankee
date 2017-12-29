using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Appliance, ICooksFood
{
    public int capacity;

    int ICooksFood.Capacity
    {
        get
        {
            return capacity;
        }
    }

    public void Preheat()
    {

    }

    //public void HeatUp()
    //{

    //}

    void ICooksFood.HeatUp()
    {
        throw new System.NotImplementedException();
    }

    void ICooksFood.Reheat()
    {
        throw new System.NotImplementedException();
    }
}
