using UnityEngine;
using System.Collections;

public class Bee : MonoBehaviour 
{
    public double weight;

    //벌 생성자는 웨이트값을 받아 생성한다.
    public Bee(double weight)
    {
        this.weight = weight;
    }

    public virtual int shiftsLeft
    {
        get { return 0; }
    }

    public virtual double GetHoneyConsumption()
    {
        double consumption;

        //벌이 일을 안할때는 7.5, 일 할때는 시간단위에 9를 더한만큼 소비한다.
        if (shiftsLeft == 0)
        {
            consumption = 7.5;
        }
        else
        {
            consumption = shiftsLeft + 9;
        }

        //벌의 무게가 150이상이면 35%이상 소비한다.
        if (weight > 150)
        {
            consumption *= 1.35;
        }

        return consumption;
    }
}
