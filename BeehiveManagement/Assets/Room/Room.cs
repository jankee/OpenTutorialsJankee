using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : Location
{
    private string decoration;
    public string Decoration
    {
        get
        {
            return decoration;
        }
    }

    public Room(string name, string decoration) : base(name)
    {
        this.decoration = decoration;
    }

    public override string Description
    {
        get
        {
            return base.Description + "You see " + Decoration + ".";
        }
    }
}
