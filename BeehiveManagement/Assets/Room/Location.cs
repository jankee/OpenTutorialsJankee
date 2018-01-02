using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Location : MonoBehaviour
{
    public Location(string name)
    {
        this.roomName = name;
    }

    public Location[] Exits;

    private string roomName;
    public string RoomName
    {
        get
        {
            return roomName;
        }
    }

    public virtual string Description
    {
        get
        {
            string description = "You're standing in the " + roomName
                + ". You see exits to the followiing place : ";

            for (int i = 0; i < Exits.Length; i++)
            {
                description += " " + Exits[i].RoomName;
                if (i != Exits.Length - 1)
                {
                    description += ",";
                }
            }
            description += ".";
            return description;
        }
    }
}
