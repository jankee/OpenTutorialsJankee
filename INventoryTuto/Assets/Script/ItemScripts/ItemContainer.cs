using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemContainer
{

    private List<Item> weapons = new List<Item>();

    public List<Item> Weapons
    {
        get { return weapons; }
        set { weapons = value; }
    }

    private List<Item> equipment = new List<Item>();

    public List<Item> Equipment
    {
        get { return equipment; }
        set { equipment = value; }
    }

    private List<Item> consumeables = new List<Item>();

    public List<Item> Consumeables
    {
        get { return consumeables; }
        set { consumeables = value; }
    }

}
