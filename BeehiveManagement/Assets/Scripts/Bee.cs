using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public virtual int ShiftsLeft
    {
        get
        {
            return 0;
        }
        
    }

    private float weight;

    public Bee(float weight)
    {
        this.weight = weight;
    }

    // 벌의 꿀 사용양 처리
    public virtual float GetHoneyConsumption()
    {
        float consumption;
        print("ShiftsLeft : " + ShiftsLeft);

        if (ShiftsLeft == 0)
        {
            consumption = 7.5f;
        }
        else
        {
            consumption = 9f + ShiftsLeft;
        }

        print("weight : " + weight);

        if (weight > 150)
        {
            consumption *= 1.35f;
        }
        return consumption;
    }

}
