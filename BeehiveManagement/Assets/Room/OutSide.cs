using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSide : Location
{
    private float hot;
    public float Hot
    {
        get
        {
            return hot;
        }
    }

    public OutSide(string name) : base(name)
    {
    }

    public override string Description
    {
        get
        {
            return base.Description;
        }
    }
}
