using UnityEngine;
using System.Collections;

public abstract class Item 
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

    public int MaxSize
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
        ItemName = itemName;
        Description = description;
        ItemType = itemType;
        Quality = quality;
        SpriteNeutral = spriteNeutral;
        SpriteHighlighted = spriteHighlighted;
        MaxSize = maxSize;
    }

    public virtual string GetTooltip()
    {
        return null;
    }

    public abstract void Use();
}
