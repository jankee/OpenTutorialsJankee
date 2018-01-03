using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit
{
    private List<Location> items = new List<Location>();
    public List<Location> Items
    {
        get
        {
            return items;
        }
        set
        {
            items = value;
        }
    }

    public List<int> selectedIndex { get; set; }
}
