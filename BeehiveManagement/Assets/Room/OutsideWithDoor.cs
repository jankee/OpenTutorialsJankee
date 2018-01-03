using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideWithDoor : OutSide, IHasExteriorDoor
{
    public OutsideWithDoor(string name, bool hot, string doorDescription):base(name, hot)
    {

    }

    private string doorDescription;
    public string DoorDescription
    {
        get
        {
            return doorDescription;
        }
    }

    private string doorLocation;
    public string DoorLocation
    {
        get
        {
            return doorLocation;
        }
        set
        {
            doorLocation = value;
        }
    }

    public override string Description
    {
        get
        {
            return base.Description + " You see " + doorDescription + ".";
        }
    }
}
