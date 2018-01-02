using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryScary : FunnyFunny, IScaryClown
{
    public ScaryScary(string funnyThingIHave, int numberOfScaryThings) : base(funnyThingIHave)
    {
        this.numberOfScaryThings = numberOfScaryThings;
    }

    private int numberOfScaryThings;

    public string ScaryThingIHave
    {
        get
        {
            return "I have " + numberOfScaryThings + " spiders";
        }
    }

    public void ScaryLittleChildren()
    {
        print("You can't have my " + base.funnyThingIHave);
    }
}
