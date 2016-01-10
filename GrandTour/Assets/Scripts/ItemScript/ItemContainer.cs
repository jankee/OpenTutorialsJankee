using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemContainer
{
    private List<Item> weapons = new List<Item>();

    private List<Item> eqipment = new List<Item>();

    private List<Item> consumeables = new List<Item>();

    public List<Item> Weapons
    {
        get { return weapons; }
        set { weapons = value; }
    }

    public List<Item> Eqipment
    {
        get { return eqipment; }
        set { eqipment = value; }
    }
    public List<Item> Consumeables
    {
        get { return consumeables; }
        set { consumeables = value; }
    }

    public ItemContainer()
    {
        //weapons.Add(new Weapon("hi", "bye", ItemType.BOOTS, Quality.EPIC, "bobo", "coco", 5, 1, 10, 12, 5, 2));
        //Debug.Log(Weapons.Count);
        //Debug.Log(weapons[0].Description);
    }
}
