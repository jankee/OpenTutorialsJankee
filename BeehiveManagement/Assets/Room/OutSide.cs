using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSide : Location
{
    private bool hot;
    public bool Hot
    {
        get
        {
            return hot;
        }
    }

    public OutSide(string name, bool hot) : base(name)
    {
        this.hot = hot;
    }

    public override string Description
    {
        get
        {
            string newDescription = base.Description;

            if (hot)
            {
                newDescription += " It's very hot.";
            }
            return newDescription;
        }
    }
}
