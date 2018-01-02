using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnyFunny : MonoBehaviour, IClown
{
    public FunnyFunny(string funnyThingIHave)
    {
        this.funnyThingIHave = funnyThingIHave;
    }

    protected string funnyThingIHave;
    public string FunnyThingIHave
    {
        get
        {
            return "Honk honk! I have " + funnyThingIHave;
        }
    }

    public void Honk()
    {
        print(FunnyThingIHave);
    }
}
