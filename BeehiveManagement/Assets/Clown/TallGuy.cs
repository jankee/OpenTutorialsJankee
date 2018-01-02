using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGuy : MonoBehaviour, IClown
{
    public string name;
    public int height;

    public string FunnyThingIHave
    {
        get
        {
            return "big shoes";
        }
    }

    public  void TalkAboutYouself()
    {
        print("My name is " + name + " and I'm " + height + "inchs tall.");
    }

    public void Honk()
    {
        print("Honk honk!");
    }
}
