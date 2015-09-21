using UnityEngine;
using System.Collections;

public class Item 
{
    public ItemType ItemType
    {
        get;
        set;
    }

    public Quality Quality
    {
        get;
        set;
    }

    public string SpriteNeutral
    {
        get;
        set;
    }


    public string SpriteHighlighted
    {
        get;
        set;
    }

    public string MaxSize
    {
        get;
        set;
    }

    public string ItemName
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    public Item()
    {

    }

    public Item(string itemName, string description, ItemType itemType, Quality quality,
        string spriteNeutral, string spriteHighlighted, int maxSize)
    {

    }
}
