using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWithDoor : Room, IHasExteriorDoor
{
    public RoomWithDoor(string name, string decoration, string doorDescription) : base(name, decoration)
    {
        this.doorDescription = doorDescription;
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
}
