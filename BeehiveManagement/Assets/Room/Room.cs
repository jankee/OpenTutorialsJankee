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

    public Room(string name) : base(name)
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
